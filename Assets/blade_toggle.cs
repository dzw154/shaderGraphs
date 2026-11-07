using UnityEngine;

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
    AudioSource audio;

    void Start()
    {
        x_scale = transform.localScale.x;
        z_scale = transform.localScale.z;
        max_scale = transform.localScale.y;
        curr_scale = max_scale;
        curr_inter = max_scale / light_delta;
        blade_state = true;
        audio.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //bool prev_state = blade_state; // 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            light_delta = blade_state ? -Mathf.Abs(light_delta) : Mathf.Abs(light_delta);
        }

        curr_scale += light_delta * Time.deltaTime;
        curr_scale = Mathf.Clamp(curr_scale, min_scale, max_scale);
        transform.localScale = new Vector3(x_scale, curr_scale, z_scale);
        blade_state = curr_scale > 0;

        if (blade_state && !blade.activeSelf)
        {
            audio.clip = saber_on;
            audio.Play();   
            blade.SetActive(true);
        }
        else if (!blade_state && blade.activeSelf)
        {
            blade.SetActive(false);
        }
    }
}
