repository TBD - ICE_GHForDevelopers

# Wright Brothers API – GitHub Hands-On Workshop

This repository provides a hands-on environment for training developers on Git and GitHub.com workflows.  
The code is a simple .NET Web API named **Wright Brothers API**, accompanied by two structured labs that guide learners through Git basics, collaboration, branch protection, pull requests, merge conflicts, and advanced workflows.

---

## How to Use These Labs

- If you’re new to GitHub or want a refresher, start with [Part 1](labs/part-1.md).  
  It covers the full round trip: clone → branch → edit → commit → push → PR → merge → sync.  

- If you already know the basics (cloning, branching, committing, pushing, opening PRs), you can jump straight to [Part 2](labs/part-2.md).  
  It focuses on collaboration features like branch protection, conflicts, CODEOWNERS, PR templates, and advanced review workflows.  

- **Bonus sections** are included in Part-2 for advanced users who want to explore GitHub Actions, Draft PRs, CODEOWNERS with teams, and issue automation.  

---

## Prerequisites

Make sure you have the following installed and configured before starting:

- **Git**, with HTTPS-based authentication to GitHub  
- **.NET SDK** (version compatible with this project)  
- A **GitHub account** with permission to push to a new repo and open pull requests  
- (Optional, but highly recommended) **VS Code / Visual Studio** or any code editor that supports C# and REST APIs.

To verify the installations, run:

```bash
git --version
dotnet --version
```

---

## Duration

* **Part-1:** \~1 hour for beginners (\~30 minutes if already familiar with Git basics)
* **Part-2:** \~1 hour for intermediate users, \~45 minutes for advanced users
* **Bonus (Part-2 Advanced Topics):** \~40 minutes if completed in full

---

## Repository Structure

```
/ (root)
├── WrightBrothersApi/         # Main Web API project
├── WrightBrothersApi.Tests/   # Unit tests for the API
├── labs/
│   ├── part-1.md              # Lab 1: Local Git round-trip
│   └── part-2.md              # Lab 2: Collaboration, conflicts, CODEOWNERS, PR templates
│                              # (includes Bonus Advanced Topics)
└── README.md                  # This file
```

---

## Getting Started

1. Clone this repository:

   ```bash
   git clone <YOUR_REPO_URL>.git
   cd <REPO_NAME>
   ```

2. Build and test the project to ensure it runs:

   ```bash
   dotnet build
   dotnet test
   ```

3. Navigate to the `labs` folder:

   ```bash
   cd labs
   ```

4. Open **part-1.md** if you’re starting from scratch.
   Or open **part-2.md** if you already know the basics.

---

## Lab Overview

* [Part 1](labs/part-1.md) focuses on Git basics
* [Part 2](labs/part-2.md) dives into collaboration tools and workflows.

## Support Tips

* Keep lab instructions open in one pane (e.g., terminal or browser) and your editor in another.
* After each step, verify the code runs and tests pass (especially after merge actions).
* Be prepared to demonstrate conflict resolution live — showing both the GitHub UI and local merge markers.

