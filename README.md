# Tower-Defence-scripts-unity
# Archived Unity Gameplay Scripts Architecture

⚠️ **Project Status: Archived Technical Prototype**  
*This repository serves as a portfolio piece showcasing my early programming foundations. It is no longer under active development.*

## 📌 Context & Portfolio Timeline

* **2018–2020 (Self-Taught Roots):** I taught myself C# and Unity, culminating in this project between 2019 and 2020. Originally intended as a commercial release, development was paused to prioritize demanding full-time career commitments and family life, including welcoming my newborn son.
* **2024–Present (Academic Acceleration):** I returned to formal software engineering by enrolling in the **Open University Q62 BSc (Hons) Computing and IT (Software Route)**. I accelerated my studies by managing a full-time 120-credit annual workload alongside my full-time job and family responsibilities. I am now entering my final year.

## 💡 Why This Repository Exists

This unfinished project stands as a foundational milestone. It proves that even during my early self-taught phase, my code quality met rigorous, clean engineering standards, demonstrating logic and structure that outpaces standard graduate expectations. It stands as a testament to my long-term passion for software development, problem-solving, and my ability to deliver under demanding constraints.

## 🛠️ Code Showcase & Technical Highlights

Because this repository only contains the codebase, the focus is entirely on data structures, algorithmic efficiency, and object-oriented design patterns.

### 1. High-Performance A* Pathfinding with Custom Min-Heap
* **File Location:** `Assets/Scripts/AstarPathfinding.cs`
* **The Challenge:** Standard A* algorithms often bottle-neck execution speeds during node sorting when relying on linear lookups or native dynamic lists ($O(N)$ complexity).
* **My Solution:** I designed and implemented a custom, generic **Binary Min-Heap data structure (`MinHeap<T> where T : IHeapItem<T>`)** to serve as the engine's pathfinding Open List. This ensures node insertions, priority tracking, and removals operate at a highly efficient **$O(\log N)$** runtime scale.

```csharp
// Excerpt from MinHeap<T> showing optimized element extraction and array-shifting
public T RemoveFirst()
{
    T first = heap[0];
    count--;
    heap[0] = heap[count];
    heap[0].Id = 0;
    ShiftDown(heap[0]); // Restores heap-property in O(log N) time

    return first;
}
```

### 2. Predictive Tower Placement & Cross-System Integration
* **File Location:** `Assets/Scripts/BuildManager.cs`
* **The Challenge:** In grid-based defense games, players can trap enemies or completely break enemy AI behavior if they are allowed to block the final route to the objective.
* **My Solution:** I engineered a predictive **State Validation system** within the `BuildManager`. Before allowing placement, the code temporarily flips the target node's state, forces the pathfinding subsystem to run a predictive check, and ensures a valid path still exists before committing to the build event.

```csharp
// Excerpt from BuildManager.cs showing defensive state checking before allocation
public bool CanBuild(Node node) 
{
    if (!node.walkalbe || node == AstarPathfinding.instance.StartPoint) 
    {
        return false;
    }
    // Simulate blocking the node to validate path integrity
    node.walkalbe = false;
    bool valid = AstarPathfinding.instance.CheckPath();
    node.walkalbe = true; // Reset state safely
    return valid;
}
```

### 3. Decoupled Spatial Mapping via Dict Lookups
* **File Location:** `Assets/Scripts/Node.cs`
* **The Challenge:** Checking world space layouts using multi-dimensional arrays restricts structural changes and slows down diagonal or coordinate evaluations.
* **My Solution:** Nodes automatically cache their physical grid orientation `Vector2` coordinates straight into a static, centralized `Dictionary<Vector2, Node>`. This enables instant **$O(1)$ spatial coordinate queries** and ultra-fast relative offset matching when scanning adjacent nodes.

```csharp
// Excerpt from Node.cs utilizing static structural mappings for dynamic neighbors
public static List<Node> GetNeighbours(Node node)
{
    List<Node> neighbours = new List<Node>();
    foreach (Vector2 direction in directions)
    {
        Vector2 currentPos = node.selfPosition + direction;
        if (_nodesMap.ContainsKey(currentPos)) // O(1) Dictionary Lookup
        {
            neighbours.Add(_nodesMap[currentPos]);
        }
    }
    return neighbours;
}
```

### 4. Memory Optimization & Polymorphic Object Pooling
* **File Location:** `Assets/Scripts/ObjectPool.cs` / `TowerBase.cs` / `EnemesScript.cs`
* **The Challenge:** Repeatedly calling `Instantiate()` and `Destroy()` for dynamic gameplay game objects (like projectiles and enemy squads) causes massive memory fragmentation and triggers Unity's garbage collector, creating runtime micro-stutters.
* **My Solution:** Implemented a centralized **Object Pool** using multi-dimensional tracking collections (`List<List<GameObject>>`) to allocate resources at startup. Entities like towers interact directly with this pipeline—using polymorphic upgrades or asynchronous `IEnumerator` routines to cycle objects cleanly off-screen without execution overhead.

```csharp
// Excerpt from TowerBase.cs showcasing pooled projectile lifecycle interaction
Projectile bullet = ObjectPool.instance.PullBullets(projectile.id).GetComponent<Projectile>();
bullet.transform.position = FirePoint.position;
bullet.SetTarget(e, Attack);
```

---

## 📜 License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details. You are free to review, modify, and reference these scripts for educational or hiring purposes.
