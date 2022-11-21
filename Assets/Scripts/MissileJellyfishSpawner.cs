using UnityEngine;

public class MissileJellyfishSpawner : MonoBehaviour
{
    public MissileJellyfish asteroidPrefab;
    public float spawnDistance = 12f;
    public float spawnRate = 0.5f;
    public int amountPerSpawn = 1;
    [Range(0f, 45f)]
    public float trajectoryVariance = 15f;
    public Wave[] waves;
 
    private Wave currentWave;
 

 
    private float timeBtwnSpawns;
    private int i = 0;
 
    private bool stopSpawning = false;
     private void Start()
    {
        SpawnWave();
    }
    private void Awake()
    {
 
        currentWave = waves[i];
        timeBtwnSpawns = currentWave.TimeBeforeThisWave;
    }
 
    private void Update()
    {
        if(stopSpawning)
        {
            return;
        }
        else if (Time.time >= timeBtwnSpawns)
        {
            SpawnWave();
            IncWave();
            timeBtwnSpawns = Time.time + currentWave.TimeBeforeThisWave;
            stopSpawning = true;
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
            Quaternion spawnRotation;
            spawnRotation = transform.rotation;
            
             // Offset the spawn point by the position of the spawner so its
            // relative to the spawner location
            spawnPoint += transform.position;

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
            stopSpawning = true;
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
