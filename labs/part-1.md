# Part 1 - Local Git round trip on GitHub.com

## 👉 Objectives

* Clone the Wright Brothers repo from GitHub.com using HTTPS
* Explore commit history and repo status locally
* Build and test the project to confirm the environment works
* Create a branch (from browser or terminal)
* Make a safe change (README)
* Stage, commit, and push the branch
* Open a Pull Request on GitHub.com
* Merge the PR into `main`
* Sync and clean up local branches
* (Optional) Link an Issue to a PR for automatic closure

## ✅ Prerequisites

* A GitHub repo that contains the Wright Brothers API code.
* Git installed locally, and you can authenticate with GitHub over HTTPS.
* .NET SDK installed, so you can build and test the project.

## ⌛ Duration
* 30 minutes for Objectives

## 0) Verify tools

First, open a terminal window:

- **In VSCode:** Press <kbd>Ctrl</kbd> + <kbd>`</kbd> (backtick) or go to **View > Terminal**.
- **On Windows:** Press <kbd>Win</kbd> + <kbd>R</kbd>, type `cmd`, and press Enter to open Command Prompt.

In your terminal, run:

```bash
git --version
dotnet --version
```

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

---

## 2) Explore the repo locally

Let's look at the recent commit history and check the current status of your files.

**View the latest commits:**  
This command shows the last 5 commits in a short format.
```bash
git log --oneline -n 5
```

**Check the status of your working directory:**  
This command shows which files have changes, which are staged, and which are untracked.
```bash
git status
```

Build and test to confirm the solution compiles on your machine.

```bash
dotnet build
dotnet test
```

Key paths in this repo include:

* `WrightBrothersApi/Controllers/PlanesController.cs`
* `WrightBrothersApi/Controllers/FlightsController.cs`
* `WrightBrothersApi/Models/*.cs`
* `WrightBrothersApi.Tests/Controllers/PlanesControllerTests.cs`

---

## 3) Create a working branch

### Option A, create the branch in the terminal

**Create a new branch called `chore/readme-note`:**  
This command creates and switches you to a new branch.
```bash
git checkout -b chore/readme-note
```

**Push the new branch to GitHub and set it to track the remote branch:**  
This command uploads your branch to GitHub and sets it as the default for future pushes.
```bash
git push -u origin chore/readme-note
```

### Option B, create the branch in the browser

**On GitHub.com:**  
Open the branch dropdown near the file list, type `chore/readme-note`, and press Enter to create the branch.

**Back in your terminal, fetch the latest branches from GitHub:**  
This command updates your local list of branches.
```bash
git fetch origin
```

**Switch to the new branch locally:**  
This command moves you to the branch you just created.
```bash
git checkout chore/readme-note
```

---

## 4) Make a small, safe change

For Part 1, use a documentation change so no build breaks.

Open `README.md` in your editor, add a short line such as:

```
Learning Git and GitHub with the Wright Brothers API.
```

**Stage your change:**  
This command tells Git to start tracking the changes you made to `README.md` and prepares it for commit.
```bash
git add README.md
```

**Commit your change with a message:**  
This command saves your staged changes to the branch with a descriptive message.
```bash
git commit -m "Docs, add workshop note to README"
```

**Push your branch and commit to GitHub:**  
This command uploads your branch and commit to your repository on GitHub.
```bash
git push
```

---

## 5) Open a Pull Request on GitHub.com

A **pull request** lets you propose changes to your code and have them reviewed before merging into the main branch.

On GitHub.com, from your forked repo, navigate to the "Pull requests" tab in the top menu.

* Click the **New pull request** button.
* Under the section "Compare and review just about anything", Select `chore/readme-note` as the compare branch.
* Confirm the base branch is `main`.
* Review the changes in the diff.  
  *The "diff" shows exactly what lines were added, changed, or removed. Double-check that only your intended changes are included.*
* Click the **Create pull request** button.
* Title it clearly, for example, `Docs, add workshop note to README`.
* Add a short description manually or use GitHub Copilot to create a summary for you.
   * Click the GitHub Copilot icon, then select **Summary**.
* Add AI as a reviewer. Under the **Reviewers** section, click the **Request** link next to the GitHub Copilot icon.
* Click the **Create pull request** button.

Review your diff in the Files changed tab to confirm only the intended lines changed.

---

## 6) Merge the Pull Request

* Since you have branch protection disabled for Part 1, you can merge straight into main.

* Scroll down, and choose the default merge option,**Merge pull request**, then click **Confirm merge**.

* Once you see the message *Pull request successfully merged and closed*, it's safe to delete the branch.
   * Click **Delete branch** to delete it after merge.

---

## 7) Sync your local repository

Before updating your local repository, it's helpful to check your current branch and status.

**Check which branch you're on:**  
This command shows your current branch.
```bash
git branch
```

**Check for any uncommitted changes:**  
This command shows if you have any changes that haven't been committed.
```bash
git status
```

Now, bring your local `main` branch up to date and clean up your local branch.

**Switch to the `main` branch:**  
This command moves you to the main branch.
```bash
git checkout main
```

**Pull the latest changes from GitHub:**  
This command updates your local main branch with any new commits from GitHub.
```bash
git pull
```

**Delete your working branch locally:**  
This command removes the `chore/readme-note` branch from your local repository.
```bash
git branch -d chore/readme-note
```

---

## 🛠 Optional Add-Ons

These exercises are not required, but help reinforce Git basics. Try them if you have extra time or want to explore beyond the core lab.

### 1. Branching from Main vs. Branching from Another Branch

So far, you created branches directly from `main`. Sometimes you’ll start a new branch while you’re already on a feature branch. This helps you see how branches can diverge.

1. You should already be on `main` after syncing your local repository.

2. **Create a new branch from `main`:**  
This command creates and switches to a new branch.
   ```bash
   git checkout -b feature/from-main
   ```

3. **Switch back to `main` and then branch from it:**  

**Switch to the main branch:**  
This command moves you back to `main`.
```bash
git checkout main
```

**Create and switch to a new branch from main:**  
This command creates a new branch and switches to it.
```bash
git checkout -b feature/second-branch
```

   Notice that both new branches point back to `main`.

4. **Branch while on a feature branch:**  

**Switch to your first feature branch:**  
This command moves you to the `feature/from-main` branch.
```bash
git checkout feature/from-main
```

**Create and switch to a nested branch:**  
This command creates a new branch from your current branch and switches to it.
```bash
git checkout -b feature/nested-branch
```

   Here, `feature/nested-branch` is created *from* `feature/from-main`, not `main`.

   You can confirm this with:

   **Show a visual graph of all branches and commits:**  
   ```bash
   git log --oneline --graph --all
   ```

5. **Explore branch history and references:**  

**Show a detailed visual graph of all branches and commits, including branch and tag names:**  
```bash
git log --oneline --graph --decorate --all
```
- `--decorate` shows branch and tag names on commits.

**List all branches with their upstream info and last commit:**  
```bash
git branch -vv
```
- `git branch -vv` lists branches with their upstream info and last commit.

For even more fun, compare all the branches:

```bash
git show-branch --all
```

**Summary**  

Understanding how to branch from `main` or from another branch gives you flexibility in your workflow. You can work on multiple features or fixes at the same time, experiment safely, and keep your changes organized. Visualizing and comparing branches helps you track progress and avoid confusion as your project grows.

---

### 2. Using Git Diff to Inspect Changes

Before committing, it’s good practice to check exactly what you’re changing. Git provides `diff` commands to see unstaged and staged edits.

**Make sure you are on the branch where you made your changes**  
For example, if you created or edited files on `feature/nested-branch`, stay on that branch.  
You do **not** need to be on `main` for these steps.

1. Edit `README.md` and add a new line:

   ```
   This is an example of using git diff.
   ```

2. **See what you changed but haven't staged yet:**  
This command shows the difference between your working directory and the last commit. You’ll see the added line prefixed with a `+`.
   ```bash
   git diff
   ```

3. **Stage your changes:**  
"Staging" means you are telling Git which changes you want to include in your next commit.  
This command adds your changes in `README.md` to the staging area.
   ```bash
   git add README.md
   ```

4. **See what is staged and ready to commit:**  
This command shows the difference between the last commit and what you have staged (ready to commit).
   ```bash
   git diff --cached
   ```

5. **Commit your staged changes:**  
This command saves your staged changes to your branch with a message describing what you did.
   ```bash
   git commit -m "Docs, demonstrate git diff"
   ```

6. **Push your commit to GitHub:**  
This command uploads your commit to your branch on GitHub.
   ```bash
   git push
   ```

**Summary**  

By using `git diff` and `git diff --cached`, you can clearly see what changes you have made and which ones are staged for commit. This helps you catch mistakes, confirm your edits, and ensure that only the intended changes are included in your commit. Reviewing your changes before committing is a key habit for maintaining code quality and avoiding accidental errors in your repository.

---

## 🎉 All done!

Congratulations! You've completed Part 1 and taken your first flight with Git and GitHub. You cloned, branched, committed, pushed, and merged like a true Wright Brother of code!

Keep exploring, keep building, and get ready for even more awesome Git adventures ahead.
