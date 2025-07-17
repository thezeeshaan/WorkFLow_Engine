# Workflow Engine - Infonetica Assignment

A minimal .NET 8 Web API that provides configurable workflow definitions and allows execution of workflows using a state-machine-like engine.

## 🚀 How to Run

1. Make sure [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) is installed

2. Clone or download this repo, then open terminal in project folder:

```bash
cd WorkflowEngine

dotnet run
```

3. The API will start at:

```
http://localhost:5000
```

---

## 📦 Sample Endpoints

### ▶️ Create Workflow

```http
POST /workflows
```

**Body (JSON):**

```json
{
  "id": "order-process",
  "states": [
    { "id": "start", "isInitial": true, "isFinal": false, "enabled": true },
    { "id": "shipped", "isInitial": false, "isFinal": true, "enabled": true }
  ],
  "actions": [
    { "id": "ship", "enabled": true, "fromStates": ["start"], "toState": "shipped" }
  ]
}
```

### 🚀 Start Workflow Instance

```http
POST /instances?definitionId=order-process
```

### 🔁 Execute Action

```http
POST /instances/{instanceId}/actions/ship
```

### 🧾 Get Instance State

```http
GET /instances/{instanceId}
```

---

## 🧠 Assumptions

* State machine is defined and stored in-memory only (no DB)
* Transitions are validated strictly as per requirements
* Only minimal error messages for clarity
* No authentication or UI provided

## ⚠️ Known Limitations

* Data is lost on server restart
* No update/delete APIs for definitions or instances
* No persistent logging/history export

---

## 📁 Project Structure

```
WorkflowEngine/
├── Models/                 # State, Action, Definition, Instance
├── Services/               # Business logic
├── Persistence/            # In-memory store
├── Controllers/            # Route handlers (Minimal APIs)
├── Program.cs              # Entry point
└── WorkflowEngine.csproj   # Project file
```

---

## ✅ Submission Notes

* All requirements from the PDF are implemented
* Code is modular and extensible
* Can be extended with Swagger, file persistence, or tests

---

Made with 💻 for the Infonetica Software Engineering Intern assignment.
