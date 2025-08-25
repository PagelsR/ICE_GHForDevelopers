# Part 1, Local Git round trip on GitHub.com

## Objectives

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

## Prerequisites

* A GitHub repo that contains the Wright Brothers API code.
* Git installed locally, and you can authenticate with GitHub over HTTPS.
* .NET SDK installed, so you can build and test the project.

## Duration
* 30 minutes

## 0) Verify tools

In your terminal, run:

```bash
git --version
dotnet --version
```

---

## 1) Clone the repository via HTTPS

On GitHub.com, copy the HTTPS URL from the green Code button, then run:

```bash
git clone https://github.com/<org-or-user>/<repo-name>.git
cd <repo-name>
```

Optionally set your Git identity if this is a new machine:

```bash
git config user.name "Your Name"
git config user.email "you@example.com"
```

Confirm the remote is set:

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

That is Part 1. If this looks good, say the word, and I will deliver **Part 2** with branch protection, a ping endpoint, CODEOWNERS, a PR template, a two-branch workflow that creates a real conflict, and clear steps to resolve it.
