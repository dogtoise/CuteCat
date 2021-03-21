using UnityEngine;
    class SimpleExplosion :MonoBehaviour
    {

    public float elapsedTime;
    public float lifeTime = 0.4f;
    private void OnEnable()
    {
        elapsedTime = 0;

    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > lifeTime)
        {
            elapsedTime = 0;
            PoolManager.ReleaseObject(this.gameObject);
        }
    }
    private void OnDisable()
    {
        elapsedTime = 0;
    }
}
