using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public enum MaskType { OFF, GONE, RED, GREEN, BLUE, CYAN, MAGENTA, YELLOW, BLACK, WHITE, NEGATIVE, GRAY, FADE };

[ExecuteInEditMode]
public class SpriteMaskScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TilemapRenderer tileRenderer;

    public MaskType type = MaskType.OFF;
    public float distance = 4f;
    public static GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) tileRenderer = GetComponent<TilemapRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("MainCamera");
        updateShader();
        toggleShader();
    }
    private int typeToInt()
    {
        if (distance <= 0 || type == MaskType.OFF) return 0;
        switch (type)
        {
            case MaskType.GONE: return 1;
            case MaskType.RED: return 2;
            case MaskType.GREEN: return 3;
            case MaskType.BLUE: return 4;
            case MaskType.CYAN: return 5;
            case MaskType.MAGENTA: return 6;
            case MaskType.YELLOW: return 7;
            case MaskType.BLACK: return 8;
            case MaskType.WHITE: return 9;
            case MaskType.NEGATIVE: return 10;
            case MaskType.GRAY: return 11;
            case MaskType.FADE: return 12;
        }
        return 0;
    }
    private void toggleShader()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0)) type = MaskType.OFF;
        if (Input.GetKeyDown(KeyCode.Keypad1)) type = MaskType.GONE;
        if (Input.GetKeyDown(KeyCode.Keypad2)) type = MaskType.RED;
        if (Input.GetKeyDown(KeyCode.Keypad3)) type = MaskType.GREEN;
        if (Input.GetKeyDown(KeyCode.Keypad4)) type = MaskType.BLUE;
        if (Input.GetKeyDown(KeyCode.Keypad5)) type = MaskType.CYAN;
        if (Input.GetKeyDown(KeyCode.Keypad6)) type = MaskType.MAGENTA;
        if (Input.GetKeyDown(KeyCode.Keypad7)) type = MaskType.YELLOW;
        if (Input.GetKeyDown(KeyCode.Keypad8)) type = MaskType.BLACK;
        if (Input.GetKeyDown(KeyCode.Keypad9)) type = MaskType.WHITE;
        if (Input.GetKeyDown(KeyCode.I)) type = MaskType.NEGATIVE;
        if (Input.GetKeyDown(KeyCode.O)) type = MaskType.GRAY;
        if (Input.GetKeyDown(KeyCode.P)) type = MaskType.FADE;
        //distance = Mathf.Clamp(distance + Input.GetAxis("Vertical"), -0.1f, 12f);// might have to comment this line out.
    }
    private void updateShader()
    {
        if (spriteRenderer == null && tileRenderer == null || target == null) return;
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        if (spriteRenderer != null) spriteRenderer.GetPropertyBlock(mpb);
        if (tileRenderer != null) tileRenderer.GetPropertyBlock(mpb);

        mpb.SetFloat("_RenderDistance", distance);
        mpb.SetFloat("_MaskTargetX", target.transform.position.x);
        mpb.SetFloat("_MaskTargetY", target.transform.position.y);
        mpb.SetFloat("_MaskType", typeToInt());

        if (spriteRenderer != null) spriteRenderer.SetPropertyBlock(mpb);
        if (tileRenderer != null) tileRenderer.SetPropertyBlock(mpb);
    }
}


