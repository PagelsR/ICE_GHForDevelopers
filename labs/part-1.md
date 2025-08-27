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

In your terminal, run:

```bash
git --version
dotnet --version
```

---

Got it, Randy. From the screenshot you shared (“Create a new fork” screen in GitHub), here are the **critical steps** attendees would need to do before starting either Part-1 or Part-2. These should go right at the top of each lab as the **starting point**.

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

Skim the latest commits and current status.

```bash
git log --oneline -n 5
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

```bash
git checkout -b chore/readme-note
git push -u origin chore/readme-note
```

### Option B, create the branch in the browser

On GitHub.com, open the branch dropdown near the file list, type `chore/readme-note`, press Enter, then return to your terminal and switch to it:

```bash
git fetch origin
git checkout chore/readme-note
```

---

## 4) Make a small, safe change

For Part 1, use a documentation change so no build breaks.

Open `README.md` in your editor, add a short line such as:

```
Learning Git and GitHub with the Wright Brothers API.
```

Stage and commit the change:

```bash
git add README.md
git commit -m "Docs, add workshop note to README"
git push
```

---

## 5) Open a Pull Request on GitHub.com

On GitHub.com, navigate to the "Pull requests" tab.

* Click the "New pull request" button.
* Select `chore/readme-note` as the compare branch.
* Confirm the base branch is `main`.
* Review the changes in the diff.
* Title it clearly, for example, `Docs, add workshop note to README`.
* Add a short description.
* Click the "Create pull request" button.

Review your diff in the Files changed tab to confirm only the intended lines changed.

---

## 6) Merge the Pull Request

* If you have branch protection disabled for Part 1, you can merge right away.
* Choose the default merge option, then confirm.
* Click the prompt to delete the remote branch after merge.

---

## 7) Sync your local repository

Bring your local `main` up to date and clean up your local branch.

```bash
git checkout main
git pull
git branch -d chore/readme-note
git push origin --delete chore/readme-note
```

---

## 8) Optional practice

Create an Issue, then close it from a PR with a commit message reference.

```bash
# After you create Issue #<number> on GitHub.com
git checkout -b fix/readme-typo
# edit README.md to fix a typo
git add README.md
git commit -m "Fix README typo, fixes #<number>"
git push -u origin fix/readme-typo
```

Open a PR for `fix/readme-typo`, verify the “Linked issues” shows the automatic closure, then merge and pull.

---

## 🛠 Optional Add-Ons

These exercises are not required but help reinforce Git basics. Try them if you have extra time or want to explore beyond the core lab.

### 1. Branching from Main vs. Branching from Another Branch

So far, you created branches directly from `main`. Sometimes you’ll start a new branch while you’re already on a feature branch. This helps you see how branches can diverge.

1. Make sure you’re on `main` and up to date:

   ```bash
   git checkout main
   git pull
   ``` 

2. Create a new branch from `main`:

   ```bash
   git checkout -b feature/from-main
   ```

3. Switch back to `main` and then branch from it:

   ```bash
   git checkout main
   git checkout -b feature/second-branch
   ```

   Notice that both new branches point back to `main`.

4. Now try branching **while on a feature branch**:

   ```bash
   git checkout feature/from-main
   git checkout -b feature/nested-branch
   ```

   Here, `feature/nested-branch` is created *from* `feature/from-main`, not `main`.

   You can confirm this with:

   ```bash
   git log --oneline --graph --all
   ```

5. Explore branch history and references**

   Try these commands to visualize branches and see more details:

   ```bash
   git log --oneline --graph --decorate --all
   git branch -vv
   ```

   - `--decorate` shows branch and tag names on commits.
   - `git branch -vv` lists branches with their upstream info and last commit.

   For even more fun, compare branches with:

   ```bash
   git show-branch --all
   ```

---

### 2. Using Git Diff to Inspect Changes

Before committing, it’s good practice to check exactly what you’re changing. Git provides `diff` commands to see unstaged and staged edits.

1. Edit `README.md` and add a new line:

   ```
   This is an example of using git diff.
   ```

2. Run:

   ```bash
   git diff
   ```

   This shows the difference between your working directory and the last commit. You’ll see the added line prefixed with a `+`.

3. Stage the file:

   ```bash
   git add README.md
   ```

4. Now run:

   ```bash
   git diff --cached
   ```

   This shows what’s staged and ready to be committed.

5. Finally, commit:

   ```bash
   git commit -m "Docs, demonstrate git diff"
   git push
   ```

These commands help you review exactly what you’re committing and reduce mistakes.

---

## 🎉 All done!

Congratulations! You've completed Part 1 and taken your first flight with Git and GitHub. You cloned, branched, committed, pushed, and merged like a true Wright Brother of code!

Keep exploring, keep building, and get ready for even more awesome Git adventures ahead.
