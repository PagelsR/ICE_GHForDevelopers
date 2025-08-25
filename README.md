# repository: ICE_GHForDevelopers

# Wright Brothers API – GitHub Hands-On Workshop

This repository provides a hands-on environment for training developers on Git and GitHub.com workflows. The code is a simple .NET Web API named **Wright Brothers API**, accompanied by two structured labs that guide learners through Git basics, collaboration, branch protection, pull requests, merge conflicts, and improvement workflows.

## Repository Structure

```

/ (root)
├── WrightBrothersApi/         # Main Web API project
├── WrightBrothersApi.Tests/   # Unit tests for the API
├── labs/
│   ├── part-1.md              # Lab 1: Local Git round-trip
│   └── part-2.md              # Lab 2: Branches, protection, conflicts, CODEOWNERS, templates
└── README.md                  # You’re here — workshop overview & setup

````

## Prerequisites

Make sure trainees have the following installed and configured:

- **Git**, with https-based authentication to GitHub.
- **.NET SDK** (version compatible with this project).
- A **GitHub account** with permission to push to a new repo and open pull requests.
- (Optional, but highly recommended) **VS Code / Visual Studio** or any code editor that supports C# and REST APIs.

To verify the installations, run:
```bash
git --version
dotnet --version
````

## Getting Started

1. Clone this repository:

   ```bash
   git clone <YOUR_REPO_URL>.git
   cd <REPO_NAME>
   ```

2. Explore the project:

   ```bash
   dotnet build
   dotnet test
   ```

3. Navigate to the `labs` folder and pick your starting point:

   ```bash
   cd labs
   ```

4. Begin with **Part 1**, open the `part-1.md` file, and follow the instructions.
   After completing Part 1 and confirming everything is green, move on to **Part 2** via the `part-2.md` file.

## Lab Overview

| Lab        | Focus                                                                                                                 |
| ---------- | --------------------------------------------------------------------------------------------------------------------- |
| **Part 1** | Git basics: clone, branch, edit, commit, push, PR, merge, sync                                                        |
| **Part 2** | Collaboration tools: branch protection, CODEOWNERS, ping endpoint, PR templates, merge conflicts, conflict resolution |

## Support Tips

* Keep lab instructions open in one pane (e.g., terminal or browser) and your editor in another.
* After each step, verify the code runs and tests pass (especially after merge actions).
* Be prepared to demonstrate conflict resolution live — showing both the GitHub UI and local merge markers.

---

Let me know if you'd like any adjustments — like adding more setup details, screenshots, or linking to GitHub Actions workflows or API documentation.
