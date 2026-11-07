using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class blade_toggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float light_delta = 0.3f;
    bool blade_state; // true = on, false = ofd
    float min_scale = 0;
    float max_scale;
    float curr_inter;
    float curr_scale;
    float x_scale;
    float z_scale;
    public GameObject blade;
    public AudioClip saber_on;
    public AudioClip saber_off;
    public AudioClip saber_hum;
    public AudioSource humSource;
    public AudioSource sfxSource;
    Renderer rend;
    Material blade_material;
    int color_id;
    int frames;

    void Start()
    {
        frames = 0;
        rend = blade.GetComponent<Renderer>();
        blade_material = rend.material;
        color_id = Shader.PropertyToID("_blade_color");

        x_scale = transform.localScale.x;
        z_scale = transform.localScale.z;
        max_scale = transform.localScale.y;
        curr_scale = max_scale;
        curr_inter = max_scale / light_delta;
        blade_state = true;
        humSource.clip = saber_hum;
        humSource.loop = true;
        humSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        //bool prev_state = blade_state; // 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float prev_inter = curr_inter;
            curr_inter= blade_state ? -Mathf.Abs(curr_inter) : Mathf.Abs(curr_inter);
            if (prev_inter != curr_inter)
            {
                if (curr_inter > 0) // if turning the blade on
                {
                    sfxSource.PlayOneShot(saber_on);
                    humSource.Play();
                }

                else // if turning the blade off
                {
                    sfxSource.PlayOneShot(saber_off);
                    humSource.Stop();
                }
            }
        }

        curr_scale += curr_inter * Time.deltaTime;
        curr_scale = Mathf.Clamp(curr_scale, min_scale, max_scale);
        transform.localScale = new Vector3(x_scale, curr_scale, z_scale);
        blade_state = curr_scale > 0;

        if (blade_state && !blade.activeSelf)
        {
            blade.SetActive(true);
        }
        else if (!blade_state && blade.activeSelf)
        {
            blade.SetActive(false);
        }

        if (blade_state && frames % 240 == 0)
        {
            frames -= 239;

            blade_material.SetColor(color_id, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        }
    }
}
