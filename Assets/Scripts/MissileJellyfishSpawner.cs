using UnityEngine;

public class MissileJellyfishSpawner : MonoBehaviour
{
    public MissileJellyfish asteroidPrefab;
    public GameManager GameManager1;
    public float spawnDistance = 12f;
    public float spawnRate = 0.5f;
    public int amountPerSpawn = 1;
    [Range(0f, 45f)]
    public float trajectoryVariance = 15f;
    public Wave[] waves;
 
    private Wave currentWave;
 

 
    private float timeBtwnSpawns;
    private int i = 0;
    private bool start = false;
 
    private bool stopSpawning = false;
     private void Start()
    {
      start =  true;

    }
    private void Awake()
    {
 
        currentWave = waves[i];
        timeBtwnSpawns = currentWave.TimeBeforeThisWave;
        SpawnWave();
    }
 
    private void Update()
    {
        if(start == true){
            start = false;
            SpawnWave();
        }
        else if(stopSpawning)
        {
            return;
        }
        else if (GameManager1.destroyed >= currentWave.NumberToSpawn * 3)
        {
            SpawnWave();
            IncWave();
            timeBtwnSpawns = Time.time + currentWave.TimeBeforeThisWave;
            GameManager1.destroyed = 0;
        } else 
        return;
    }    


    private void SpawnWave()
    {
        for (int i = 0; i < currentWave.NumberToSpawn; i++)
        {
            int num = Random.Range(0, currentWave.EnemiesInWave.Length);
            
Vector2 spawnDirection = Random.insideUnitCircle.normalized;
Vector3 spawnPoint = spawnDirection * spawnDistance;
            while(((transform.position.y > 6 || transform.position.y < -6) || (transform.position.x > 14 || transform.position.x < -14)) == true){ 
            spawnDirection = Random.insideUnitCircle.normalized;
            spawnPoint = spawnDirection * spawnDistance;
            spawnPoint += transform.position;
            }
            
            Quaternion spawnRotation;
            spawnRotation = transform.rotation;
            
             // Offset the spawn point by the position of the spawner so its
            // relative to the spawner location
            
            

            // Calculate a random variance in the MissileJellyfish's rotation which will
            // cause its trajectory to change
            
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            MissileJellyfish MissileJellyfish = Instantiate(asteroidPrefab, spawnPoint, rotation);
            MissileJellyfish.size = Random.Range(MissileJellyfish.minSize, MissileJellyfish.maxSize);
            Vector2 trajectory = rotation * -spawnDirection;

            
            MissileJellyfish.SetTrajectory(trajectory);
            
            Instantiate(currentWave.EnemiesInWave[num], spawnPoint, spawnRotation);
        }
    }
        private void IncWave()
    {
        if (i + 1 < waves.Length)
        {
            i++;
            currentWave = waves[i];
        }
        else
        {
            SpawnWave();
        }
    }
    
    }
/*     public void Spawn()
    {
        for (int i = 0; i < amountPerSpawn; i++)
        {
            // Choose a random direction from the center of the spawner and
            // spawn the MissileJellyfish a distance away
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * spawnDistance;

            // Offset the spawn point by the position of the spawner so its
            // relative to the spawner location
            spawnPoint += transform.position;

            // Calculate a random variance in the MissileJellyfish's rotation which will
            // cause its trajectory to change
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Create the new MissileJellyfish by cloning the prefab and set a random
            // size within the range
            MissileJellyfish MissileJellyfish = Instantiate(MissileJellyfishPrefab, spawnPoint, rotation);
            MissileJellyfish.size = Random.Range(MissileJellyfish.minSize, MissileJellyfish.maxSize);

            // Set the trajectory to move in the direction of the spawner
            Vector2 trajectory = rotation * -spawnDirection;
            MissileJellyfish.SetTrajectory(trajectory);
        }
    }

} */
