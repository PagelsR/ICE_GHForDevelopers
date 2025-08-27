# Part 2- Branches, Protections, and Conflicts

## 👉 Objectives

* Create branches in browser and terminal
* Set branch protection on `main`
* Make changes on two branches and open PRs
* Merge one PR to create a real merge conflict in the other
* Resolve the conflict in the browser or locally
* Add a `CODEOWNERS` file and a `pull_request_template.md`
* Add a `ping` endpoint to `PlanesController`

## ✅ Prerequisites

* Completion of [Part 1](labs/part-1.md) (or prior experience with cloning, branching, committing, pushing, and creating PRs).
* You can push to the repo
* Default branch is `main`

## ⌛ Duration
* 30 minutes for Objectives
* 45 minutes for Bonus Labs

---

## 0) 🟢 Starting Point: Fork the Repository

Before beginning this lab, make your own copy of the repository on GitHub:

1. Click **Fork** on the repo page.
2. Under **Owner**, make sure your GitHub username is selected.
3. Leave the **Repository name** as suggested (e.g., `IDTC.pipelines`) or customize if needed.
4. (Optional) Add a short description, e.g., *ICE Digital Trust – Custody pipelines*.
5. Ensure **“Copy the main branch only”** is checked.
6. Click **Create fork**.

You now have your own fork of the repository. All lab work will be done inside **your fork**, not the original repository.


## 1) Clone the repository via HTTPS

On GitHub.com, copy the HTTPS URL from the green Code button, then run:

```bash
git clone https://github.com/<your-username>/<your-forked-repo>.git
cd <your-forked-repo>
```

Optionally set your Git identity if this is a new machine:

```bash
git config user.name "Your Name"
git config user.email "you@example.com"
```

Verify you are in your fork by running:

```bash
git remote -v
```

Confirm you are on the latest `main` by running:

```bash
git checkout main
git pull
```

---

## 1) Create two feature branches

### Option A, Terminal

```bash
git checkout -b feature/tune-logging
git push -u origin feature/tune-logging

git checkout main
git checkout -b feature/add-ping
git push -u origin feature/add-ping
```

### Option B, Browser

* Use the branch selector on GitHub.com to create `feature/tune-logging` and `feature/add-ping`, then in the terminal:

```bash
git fetch origin
git checkout feature/tune-logging
```

---

## 2) Set branch protection on `main` in the browser

In your repo on GitHub.com:

1. Settings, Branches
2. Add rule, Branch name pattern `main`
3. Select these options and save:

   * Require a pull request before merging
   * Require approvals, set to 1
   * Dismiss stale pull request approvals when new commits are pushed
   * Status checks optional if you do not have Actions yet

---

## 3) Branch A, tune a logging line and open PR A

Edit the logging line in `WrightBrothersApi/Controllers/PlanesController.cs`.

Find:

```csharp
_logger.LogInformation("Debug: GET all ✈✈✈ NO PARAMS ✈✈✈");
```

Change it to:

```csharp
_logger.LogInformation("API log, fetching all planes");
```

Commit and push:

```bash
git add WrightBrothersApi/Controllers/PlanesController.cs
git commit -m "Tune logging, clarify GET all message"
git push
```

Open a PR from `feature/tune-logging` into `main`. Title it:

```
Tune logging for GET /planes
```

Leave PR A open for review.

---

## 4) Branch B, create ping endpoint, different logging text, CODEOWNERS, PR template

Switch to Branch B:

```bash
git checkout feature/add-ping
```

### 4a) Add a `ping` endpoint to `PlanesController`

Open `WrightBrothersApi/Controllers/PlanesController.cs`, then add this action inside the controller class:

```csharp
[HttpGet("ping")]
public IActionResult Ping()
{
    return Ok("Planes API is alive");
}
```

Also change the same logging line, but to a different string to set up the conflict:

```csharp
_logger.LogInformation("Retrieving fleet inventory");
```

### 4b) Add a CODEOWNERS file

Create `.github/CODEOWNERS` with this starter content:

```text
# All C# files require review from the API team
*.cs    @your-org/api-team

# Specific ownership example
WrightBrothersApi/Controllers/FlightsController.cs @your-org/flights-team
```

### 4c) Add a pull request template

Create `.github/pull_request_template.md`:

```markdown
## Summary
Describe what changed and why.

## Testing
List how you tested, commands run, and results.

## Risks / Rollback
Any potential risks, and how to revert if needed.

## Linked Issue
Fixes #<issue-number>  <!-- Replace with the actual issue if applicable -->
```

Commit and push the Branch B changes:

```bash
git add WrightBrothersApi/Controllers/PlanesController.cs .github/CODEOWNERS .github/pull_request_template.md
git commit -m "Add ping endpoint, adjust logging, add CODEOWNERS and PR template"
git push
```

Open PR B from `feature/add-ping` into `main`. Title it:

```
Add ping endpoint, adjust logging, add CODEOWNERS and PR template
```

---

## 5) Merge PR A to create a real conflict in PR B

* Merge **PR A**: “Tune logging for GET /planes” into `main`.
* Return to **PR B**. You should now see a banner:

  ```
  This branch has conflicts that must be resolved
  ```

---

## 6) Resolve the merge conflict

### Option A, Resolve in the browser

1. In PR B, click **Resolve conflicts**
2. In `PlanesController.cs` you will see markers like:

   ```
   <<<<<<< main
   _logger.LogInformation("API log, fetching all planes");
   =======
   _logger.LogInformation("Retrieving fleet inventory");
   >>>>>>> feature/add-ping
   ```
3. Pick a final version, remove the markers, and keep valid code only. For example:

   ```csharp
   _logger.LogInformation("Retrieving fleet inventory");
   ```
4. Mark as resolved, commit the merge, wait for checks if any, then **Merge pull request**.

### Option B, Resolve locally

```bash
git checkout feature/add-ping
git fetch origin
git merge origin/main
# You will see CONFLICT messages
# Edit the file and keep a single LogInformation line

git add WrightBrothersApi/Controllers/PlanesController.cs
git commit -m "Resolve logging conflict in PlanesController"
git push
# Return to PR B in GitHub and complete the merge
```

---

## 7) Verify locally and clean up

```bash
git checkout main
git pull
dotnet build
dotnet test
```

Optional local branch cleanup:

```bash
git branch -d feature/tune-logging feature/add-ping
git push origin --delete feature/tune-logging
git push origin --delete feature/add-ping
```

---

## Where to see merge conflicts

* **PR banner on GitHub** shows “This branch has conflicts” with a Resolve button
* **Files changed** tab flags conflicted files
* **Local terminal** after `git merge` prints `CONFLICT` and the affected paths
* Files contain conflict markers:

  ```
  <<<<<<< HEAD
  your branch changes
  =======
  incoming changes
  >>>>>>> origin/main
  ```

---

## 🔥 Bonus Labs (Advanced Topics)

These exercises are optional but demonstrate more advanced GitHub workflows. Try them if you finish early or want to explore real-world practices.

### 1. Enforce Linear History
Update your branch protection rule on `main`:
1. Go to **Settings → Branches → main branch rule**.
2. Enable **Require linear history**.
3. Save.

**What it does:** PRs can now only be merged using **Squash** or **Rebase**. The “Create a merge commit” option is disabled.  
Try opening a new branch, making a change, and then opening a PR. You’ll notice the merge options are restricted.

---

### 2. Require Status Checks with GitHub Actions
Add a simple build workflow in `.github/workflows/build.yml`:

```yaml
name: Build
on: [pull_request]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 8.0.x
      - run: dotnet build --configuration Release
```

Commit and push this workflow to `main`.
Then update your branch protection rule:

* **Require status checks to pass before merging**
* Select the `Build` workflow.

**Test it:**

1. Open a new PR.
2. Notice that you cannot merge until the `Build` workflow completes successfully.

---

### 3. Expand CODEOWNERS with Teams and Individuals

Edit `.github/CODEOWNERS` to show ownership at both the team and individual level:

```text
# Controllers owned by the API team
WrightBrothersApi/Controllers/* @your-org/api-team

# Models require review from specific devs
WrightBrothersApi/Models/* @dev1 @dev2
```

Commit and push.
Now, open a PR that changes both a controller and a model.
Observe that GitHub automatically requests reviews from both the team and the individuals listed.

---

### 4. Use Draft Pull Requests

When opening a PR, select **Create as draft** instead of “Open pull request.”
This communicates that the work is not ready for review yet.

**Test it:**

* Open a draft PR.
* Observe the green merge button is disabled and reviewers cannot approve.
* Convert it to a regular PR using the “Ready for review” button.

---

### 5. Automate Issue Closure with PR Keywords

Create a new Issue on GitHub.com (for example, “Update documentation”).
In a PR description or commit message, include:

```
Fixes #<issue-number>
```

**Test it:**

* Merge the PR.
* The linked Issue will automatically close when the PR merges.

---

## Wrap-up

These advanced topics show how GitHub can enforce process consistency (linear history, required checks), improve collaboration (CODEOWNERS, draft PRs), and automate project management (issue auto-closing).
They are optional, but highly valuable for teams working at scale.

---

## 🎉 All done!

Awesome work! You've conquered branching, protection rules, merge conflicts, and advanced GitHub workflows.

 You're now flying high with pro-level Git skills—ready to tackle any repo challenge! Keep pushing boundaries and collaborating like a true code pioneer.

