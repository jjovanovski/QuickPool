# QuickPool
Tiny Unity library for pooling mono behaviours or scripts.

## Installation
Just copy the <b>QuickPool</b> directory into your <b>Scripts</b> directory.
The scripts are under the following namespace:
```
using QuickPool;
```

## Pools
There are two types of pools:
1. <b>Object pool (MonoBehaviours)</b> - pool for game objects which need to be instantiated into the scene.
2. <b>Script pools (any class)</b> - pool for general classes which can be created with the <b>new</b> operator.

## Object pool
Creating new object pool:
```c#
var pool = Pool.CreateObjectPool<T>(objectPrefab, objectsParent, initialCapacity, maxCapacity, resetAction);
```
Parameters explanation:
1. objectPrefab - the prefab you want to pool
2. objectsParent - reference to the Transform in which the objects will be instantiated
3. initialCapacity - how many objects to instantiate initally
4. maxCapacity - the maximum number of objects to be kept in the pool. If the maximum capacity is reached when trying to return object to the bool, the object can be automatically or manually destroyed. -1 means no maximum capacity.
5. resetAction - Action<T> which will be executed each time an object is fetched from the pool

Example:
```c#
public Bullet bulletPrefab;
public Transform bulletsParent;

private ObjectPool<Bullet> bulletPool;

void Start() {
  bulletPool = Pool.CreateObjectPool<Bullet>(bulletPrefab, bulletsParent, 10, -1, (bullet) => {
    bullet.transform.position = Vector3.zero;
  });
}

void ShootBullet() {
  Bullet bullet = bulletPool.Get();
  bullet.Shoot();
}

void PoolBullet(Bullet bullet) {
  pool.Pool(bullet);
}

```

## Script pool
