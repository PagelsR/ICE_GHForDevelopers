# Part 2, Branches, Protections, Conflicts, CODEOWNERS, PR Template, Ping

## Objectives

* Create branches in browser and terminal
* Set branch protection on `main`
* Make changes on two branches and open PRs
* Merge one PR to create a real merge conflict in the other
* Resolve the conflict in the browser or locally
* Add a `CODEOWNERS` file and a `pull_request_template.md`
* Add a `ping` endpoint to `PlanesController`

## Prerequisites

* Part 1 is complete
* You can push to the repo
* Default branch is `main`

## Duration
* 30 minutes

---

## 0) Confirm you are on the latest `main`

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
