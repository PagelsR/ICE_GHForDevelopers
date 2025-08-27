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

**View your branches on GitHub.com:**  
1. Go to your repository page on GitHub.com.
2. Click the **Branch** dropdown near the top left.
3. You should see `main`, `feature/tune-logging`, and `feature/add-ping` listed.
4. This confirms your branches were created and pushed successfully.

---

## 2) Set branch protection on `main` in the browser

Branch protection helps enforce code quality and collaboration standards. It prevents direct pushes to important branches like `main`, requires code review, and can enforce automated checks before merging. This is essential for teams to avoid accidental overwrites, ensure peer review, and maintain a reliable codebase.

In your repo on GitHub.com:

1. Go to **Settings > Rules > Rulesets**

2. Click  **New ruleset**, select **New branch ruleset** to protect the `main` branch.

3. Name it `Protect main branch` or anything you want.

  ```
  Protect main branch
  ```

4. Set **Enforcement status** to **Active**.

5. Set **Target branches** by clicking **Add target**, then select **Include default branch**.

6. Select these options:

   * [ ] **Require a pull request before merging**
   * [ ] **Require approvals**, set to **1**
   * [ ] **Dismiss stale pull request approvals when new commits are pushed**

7. Click **Create** to save your changes.

---

## 3) Branch A, tune a logging line and open PR A

Return to your terminal window:

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

On GitHub.com, go to your repo's **Pull Requests** tab.

Under section *Compare and review just about anything*, select the compare branch to `feature/tune-logging`.

- Ensure the base branch to `main`.

* Click the **Create pull request** button.

* Title it clearly, for example, `Tune logging for GET /planes`.

  ```
  Tune logging for GET /planes
  ```

* Add a short description manually or use GitHub Copilot to create a summary for you.
   * Click the GitHub Copilot icon, then select **Summary**.

* Add AI as a reviewer. Under the **Reviewers** section, click the **Request** link next to the GitHub Copilot icon.

* Click the **Create pull request** button.

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

Open `WrightBrothersApi/Controllers/PlanesController.cs`, add this action inside the controller class inbetween other endpoints:

```csharp
[HttpGet("ping")]
public IActionResult Ping()
{
    return Ok("Planes API is alive");
}
```

Also change the same logging line (from previous step, i.e. line #48), but to a different string to set up the conflict:

```csharp
_logger.LogInformation("Retrieving fleet inventory");
```

**Stage your change:**  
This command tells Git to start tracking the changes you made to `PlanesController.cs` and prepares it for commit.
```bash
git add WrightBrothersApi/Controllers/PlanesController.cs
```

**Commit your change with a message:**  
This command saves your staged changes to the branch with a descriptive message.
```bash
git commit -m "Add ping endpoint and adjust logging"
```

**Push your branch and commit to GitHub:**  
This command uploads your branch and commit to your repository on GitHub.
```bash
git push
```

### 4b) Add a CODEOWNERS file

On GitHub.com, navigate to your repository and open the file browser.

**Make sure you are working in your `feature/add-ping` branch:**
1. At the top left, click the **Branch** dropdown.
2. Select `feature/add-ping` from the list.  
   If you do not see it, use the search box to find and select `feature/add-ping`.

**To add a new file at `.github/CODEOWNERS`:**
1. Click the **Add file** button, then select **Create new file**.
2. Enter `.github/CODEOWNERS` as the filename.

  ```
  .github/CODEOWNERS
  ```

3. Paste the starter content below into the editor.

```text
# All C# files require review from the API team
*.cs    @your-org/api-team

# Specific ownership example
WrightBrothersApi/Controllers/FlightsController.cs @your-org/flights-team
```

> [!IMPORTANT]
> To avoid errors, ensure that every owner listed in your CODEOWNERS file:
> - Is a valid GitHub username or team.
> - For teams, use the format `@org/team-name` (replace `org` with your actual organization name).
> - The team must exist, be publicly visible, and have write access to the repository.
> - For individuals, use their GitHub username (e.g., `@octocat`).
> - If you use placeholder names like `@your-org/api-team`, replace them with real team names from your organization.
> - You can check team names and visibility in your organization settings on GitHub.

> If you see "Unknown owner" errors, double-check the spelling, organization, and permissions for each owner listed.

4. Click **Commit changes** in upper right hand corner to commit.

5. Confirm option **Commit directly to the feature/add-ping branch** is selected, not `main`.

6. Click **Commit changes**.

### 4c) Add a pull request template

**Stay on your feature branch (`feature/add-ping`) for this change.**

**To add a new file at `.github/pull_request_template.md`:**
1. Click the **Add file** button, then select **Create new file**.

2. Since you're already in folder **.github**, enter `pull_request_template.md` as the filename.

  ```
  pull_request_template.md
  ```

3. Paste the starter content below into the editor.

```markdown
## Summary
Describe what changed and why.

## Testing
List how you tested, commands run, and results.

## Risks / Rollback
Any potential risks, and how to revert if needed.

## Linked Issue on Jira
Fixes Jira #<issue-number>  <!-- Replace with the actual issue if applicable -->
```

4. Click **Commit changes** in upper right hand corner to commit.

5. Confirm option **Commit directly to the feature/add-ping branch** is selected, not `main`.

6. Click **Commit changes**.

On GitHub.com, go to your repo's **Pull Requests** tab.

Under section *Compare and review just about anything*, select the compare branch to `feature/add-ping`.

- Ensure the base branch to `main`.

* Click the **Create pull request** button.

* Title it clearly, for example, `Add ping endpoint, adjust logging, add CODEOWNERS and PR template`.

  ```
  Add ping endpoint, adjust logging, add CODEOWNERS and PR template
  ```

* Add a short description manually or use GitHub Copilot to create a summary for you.
   * Click the GitHub Copilot icon, then select **Summary**.

* Add AI as a reviewer. Under the **Reviewers** section, click the **Request** link next to the GitHub Copilot icon.

* Click the **Create pull request** button.

---

## 5) Merge PR A to create a real conflict in PR B

At this stage, you will merge the first pull request (PR A) into `main`. This will introduce a change that conflicts with the work in your second pull request (PR B), allowing you to experience and resolve a real-world merge conflict scenario.

**Steps:**
1. Click the **Pull Requests** tab in your repository on GitHub.com.

2. You should see two open PRs:  
   - **PR A**: "Tune logging for GET /planes"  
   - **PR B**: "Add ping endpoint, adjust logging, add CODEOWNERS and PR template"

3. Click on **PR A** to review it.

4. If GitHub Copilot or other reviewers have made "commit suggestions," feel free to resolve or apply them before merging. This is a great opportunity to practice handling suggested changes in a real workflow.

5. Click **Merge pull request**, then **Confirm merge** to merge it into `main`.

6. Once you see the message *Pull request successfully merged and closed*, it's safe to delete the branch.
   * Click **Delete branch** to delete it after merge.

7. After merging PR A, return to PR B.  
   - You will see a message near the bottom of PR B indicating a conflict:
     ```
     This branch has conflicts that must be resolved
     ```

8. Click **Resolve conflicts** to begin the conflict resolution process.

---

## 6) Resolve the merge conflict

### Option A, Resolve in the browser

1. In PR B, click **Resolve conflicts**

2. In `PlanesController.cs` you will see markers similar to this:

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

1. **Switch to your feature branch:**
   ```bash
   git checkout feature/add-ping
   ```

2. **If you have local changes, commit them (recommended):**
   ```bash
   git add WrightBrothersApi/Controllers/PlanesController.cs
   git commit -m "Save local changes before merging"
   ```
   *Or, if you do not want to commit, stash them:*
   ```bash
   git stash
   ```
   > `git stash` temporarily saves your changes so you can safely pull and merge. Restore them later with `git stash pop`.

3. **Pull the latest changes for your feature branch:**
   ```bash
   git pull
   ```

4. **Merge the latest `main` into your feature branch:**
   ```bash
   git merge origin/main
   ```
   *If there are conflicts, Git will notify you.*

5. **Edit the conflicted file(s):**
   - Open `WrightBrothersApi/Controllers/PlanesController.cs` and resolve conflict markers (`<<<<<<<`, `=======`, `>>>>>>>`).

6. **Stage and commit the resolved file(s):**
   ```bash
   git add WrightBrothersApi/Controllers/PlanesController.cs
   git commit -m "Resolve logging conflict in PlanesController"
   ```

7. **If you stashed changes earlier, restore them:**
   ```bash
   git stash pop
   ```

8. **Push your changes to GitHub:**
   ```bash
   git push
   ```

9. **Return to PR B in GitHub and complete the merge.**

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

> **Troubleshooting:**  
> If you do not see a merge conflict banner in PR B after merging PR A, check that:
> - Both branches were created from the same starting commit on `main`.
> - Both PRs modify the exact same line in the same file.
> - You did not merge `main` into your feature branch before merging PR A.
>  
> If needed, re-create the branches and repeat the steps, ensuring both PRs change the same line in `WrightBrothersApi/Controllers/PlanesController.cs` with different strings.


