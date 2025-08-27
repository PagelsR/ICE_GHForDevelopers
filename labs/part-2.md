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

First, open a terminal window:

- **In VSCode:** Press <kbd>Ctrl</kbd> + <kbd>`</kbd> (backtick) or go to **View > Terminal**.
- **On Windows:** Press <kbd>Win</kbd> + <kbd>R</kbd>, type `cmd`, and press Enter to open Command Prompt.

### Option A, Terminal

**Create and switch to the first feature branch:**  
This command creates a new branch called `feature/tune-logging` and switches to it.
```bash
git checkout -b feature/tune-logging
```

**Push the new branch to GitHub and set it to track the remote branch:**  
This uploads your branch to GitHub and sets it as the default for future pushes.
```bash
git push -u origin feature/tune-logging
```

**Switch back to the main branch:**  
This command moves you to the `main` branch.
```bash
git checkout main
```

**Create and switch to the second feature branch:**  
This command creates a new branch called `feature/add-ping` and switches to it.
```bash
git checkout -b feature/add-ping
```

**Push the new branch to GitHub and set it to track the remote branch:**  
This uploads your branch to GitHub and sets it as the default for future pushes.
```bash
git push -u origin feature/add-ping
```

### Option B, Browser

* Use the branch selector on GitHub.com to create `feature/tune-logging` and `feature/add-ping`.

**Fetch the latest branches from GitHub:**  
This updates your local list of branches.
```bash
git fetch origin
```

**Switch to the first feature branch locally:**  
This command moves you to the `feature/tune-logging` branch.
```bash
git checkout feature/tune-logging
```

---

## 2) Set branch protection on `main` in the browser

Branch protection helps enforce code quality and collaboration standards. It prevents direct pushes to important branches like `main`, requires code review, and can enforce automated checks before merging. This is essential for teams to avoid accidental overwrites, ensure peer review, and maintain a reliable codebase.

In your repo on GitHub.com:

1. Go to **Settings > Branches**
2. Add a **Ruleset** with branch name pattern `main`
3. Select these options and save:

   * Require a pull request before merging
   * Require approvals, set to 1
   * Dismiss stale pull request approvals when new commits are pushed
   * Status checks optional if you do not have Actions yet

---

## 3) Branch A, tune a logging line and open PR A

**Make sure you are on the correct branch:**  
Check your current branch to confirm you are on `feature/tune-logging` before making changes.

```bash
git branch
```

If needed, switch to the branch:

```bash
git checkout feature/tune-logging
```

**Edit the logging line in** `WrightBrothersApi/Controllers/PlanesController.cs`

Find the following line (i.e. line #48):
```csharp
_logger.LogInformation("Debug: GET all ✈✈✈ NO PARAMS ✈✈✈");
```

**Change it to a clearer message:**
```csharp
_logger.LogInformation("API log, fetching all planes");
```

**Stage the file:**  
This command adds your changes to the staging area, preparing them for commit.
```bash
git add WrightBrothersApi/Controllers/PlanesController.cs
```

**Commit your change with a descriptive message:**  
This command saves your staged changes to your branch.
```bash
git commit -m "Tune logging, clarify GET all message"
```

**Push your commit to GitHub:**  
This uploads your commit to the remote branch.
```bash
git push
```

**Open a Pull Request (PR) from `feature/tune-logging` into `main`:**  
On GitHub.com, go to your repo's Pull Requests tab and click **New pull request**.

- Set the base branch to `main` and the compare branch to `feature/tune-logging`.
- Title your PR:  
  ```
  Tune logging for GET /planes
  ```
- Optionally, add a description explaining why you made this change.  
  You can use GitHub Copilot to help summarize your PR:  
  * Click the GitHub Copilot icon, then select **Summary** to auto-generate a description.

Leave PR A open for review. This allows others to review your changes and sets up the scenario for a merge conflict in the next steps.

---

## 4) Branch B, create ping endpoint, different logging text, CODEOWNERS, PR template

**Make sure you are on the correct branch:**  
Check your current branch to confirm you are on `feature/add-ping` before making changes.

```bash
git branch
```

If needed, switch to the branch:

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

Also change the same logging line (from previous step), but to a different string to set up the conflict:

```csharp
_logger.LogInformation("Retrieving fleet inventory");
```

### 4b) Add a CODEOWNERS file

**Make sure you are on your feature branch (`feature/add-ping`) before making changes.**  
Branch protection on `main` prevents direct changes, so all edits must be made in a feature branch.

Check your current branch:
```bash
git branch
```
If needed, switch to the branch:
```bash
git checkout feature/add-ping
```

On GitHub.com, navigate to your repository and open the file browser.

**Create a new file at `.github/CODEOWNERS` with this starter content:**
```text
# All C# files require review from the API team
*.cs    @your-org/api-team

# Specific ownership example
WrightBrothersApi/Controllers/FlightsController.cs @your-org/flights-team
```

### 4c) Add a pull request template

**Stay on your feature branch (`feature/add-ping`) for this change.**

On GitHub.com, navigate to your repository and open the file browser.

**Create a new file at `.github/pull_request_template.md` with this starter content:**
```markdown
## Summary
Describe what changed and why.

## Testing
List how you tested, commands run, and results.

## Risks / Rollback
Any potential risks, and how to revert if needed.

## Linked Issue on Jira
Fixes #<issue-number>  <!-- Replace with the actual issue if applicable -->
```

**Stage your changes:**  
This command adds all your new and changed files to the staging area, preparing them for commit.
```bash
git add WrightBrothersApi/Controllers/PlanesController.cs .github/CODEOWNERS .github/pull_request_template.md
```

**Commit your changes with a descriptive message:**  
This command saves your staged changes to your feature branch.
```bash
git commit -m "Add ping endpoint, adjust logging, add CODEOWNERS and PR template"
```

**Push your changes to GitHub:**  
This uploads your commit to the remote feature branch.
```bash
git push
```

**Open PR B** from `feature/add-ping` into `main`

On GitHub.com, go to your repo's Pull Requests tab and click **New pull request**.

- Set the base branch to `main` and the compare branch to `feature/add-ping`.
- Title your PR:  
  ```
  Add ping endpoint, adjust logging, add CODEOWNERS and PR template
  ```
- Optionally, use GitHub Copilot to help summarize your PR.

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

**Switch to your feature branch:**  
This command makes sure you are working on the correct branch.
```bash
git checkout feature/add-ping
```

**Fetch the latest changes from GitHub:**  
This updates your local repository with the latest commits from the remote.
```bash
git fetch origin
```

**Merge the latest `main` into your feature branch:**  
This command attempts to merge changes from `main` into your branch. If there are conflicting changes, Git will notify you.
```bash
git merge origin/main
```
You will see `CONFLICT` messages if there are merge conflicts.

**Edit the conflicted file:**  
Open `WrightBrothersApi/Controllers/PlanesController.cs` in your editor.

Look for conflict markers (`<<<<<<<`, `=======`, `>>>>>>>`) and resolve the conflict by keeping only the correct `LogInformation` line.

**Stage the resolved file:**  
This command marks the conflict as resolved and prepares the file for commit.
```bash
git add WrightBrothersApi/Controllers/PlanesController.cs
```

**Commit the merge resolution:**  
This command saves your resolved changes to your branch.
```bash
git commit -m "Resolve logging conflict in PlanesController"
```

**Push your changes to GitHub:**  
This uploads your resolved merge to the remote branch.
```bash
git push
```

**Return to PR B in GitHub and complete the merge:**  
Go to your Pull Request on GitHub.com, verify the conflict is resolved, and finish merging.

---

## 7) Verify locally and clean up

In this section, you'll verify that your local repository is up to date with the latest changes from `main`, confirm your code builds and tests successfully, and then clean up your feature branches both locally and on GitHub.

```bash
git checkout main
```
- Switches to the `main` branch.

```bash
git pull
```
- Updates your local `main` branch with the latest changes from GitHub.

```bash
dotnet build
```
- Builds your project to ensure there are no compilation errors.

```bash
dotnet test
```
- Runs your automated tests to confirm everything works as expected.

Optional local branch cleanup:

```bash
git branch -d feature/tune-logging feature/add-ping
```
- Deletes the local copies of your feature branches.

```bash
git push origin --delete feature/tune-logging
git push origin --delete feature/add-ping
```
- Deletes the remote copies of your feature branches on GitHub.

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
Update your branch protection ruleset on `main`:
1. Go to **Settings → Rules → Rulesets**.
2. Enable **Require linear history**.
3. Save.

**What it does:** PRs can now only be merged using **Squash** or **Rebase**. The “Create a merge commit” option is disabled.  

Try opening a new branch, making a change, and then opening a PR. You’ll notice the merge options are restricted.

---

### 2. Expand CODEOWNERS with Teams and Individuals

> [!IMPORTANT]  
> The `main` branch is protected to ensure code quality and collaboration standards.  
> To update files such as `.github/CODEOWNERS`, first create and work in a feature branch.  
> Once your changes are ready, open a Pull Request to merge them into `main`—this maintains a secure and professional workflow.

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

### 3. Use Draft Pull Requests

When opening a PR, select **Create as draft** instead of **Open pull request**.
This communicates that the work is not ready for review yet.

**Test it:**

* Open a draft PR.
* Observe the green merge button is disabled and reviewers cannot approve.
* Convert it to a regular PR using the “Ready for review” button.

---

## Wrap-up

These advanced topics show how GitHub can enforce process consistency (linear history, required checks), improve collaboration (CODEOWNERS, draft PRs), and automate project management (issue auto-closing).
They are optional, but highly valuable for teams working at scale.

---

## 🎉 All done!

Awesome work! You've conquered branching, protection rules, merge conflicts, and advanced GitHub workflows.

 You're now flying high with pro-level Git skills—ready to tackle any repo challenge! Keep pushing boundaries and collaborating like a true code pioneer.

