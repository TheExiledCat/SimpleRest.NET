# Contributing

We welcome contributions to this repository! To ensure a smooth and efficient process, please follow the guidelines below:

## 1. **Discussions & Issue Creation**
Before making a contribution, always **start with an issue** to discuss the feature, bug, or change you want to implement. This ensures that your idea aligns with the project’s goals, and it provides clarity for others on the progress.

- **Create an issue** on the repository.
- **Describe the issue or idea clearly**, including context, motivation, and any relevant information that will help maintainers understand it.
- **Tag relevant maintainers** in the issue if needed.
- **Discuss with maintainers**: Once the issue has been discussed and agreed upon, a maintainer will confirm and assign the task or feature to you.

## 2. **Forking the Repository**
After the issue has been discussed and approved, **fork the repository** to start your work.

- **Fork the repository** to your own GitHub account.
- Make sure to **sync your fork** with the upstream repository (i.e., the main project repository) regularly to avoid conflicts.

## 3. **Branching Strategy**
Create a new branch from the base branch (usually `main` or `develop`), and name it appropriately based on the type of work you are doing.

- **Branch naming conventions**:
  - `fix/` for bug fixes (e.g., `fix/issue-123`)
  - `feature/` for new features (e.g., `feature/add-user-login`)
  - `docs/` for documentation updates (e.g., `docs/update-readme`)
  - `tests/` for adding or updating tests (e.g., `tests/add-login-tests`)
  - `templates/` for changes to templates or other project configurations (e.g., `templates/update-ci-config`)
  
By using consistent naming conventions, it's easier for maintainers and other contributors to understand the purpose of your branch at a glance.

## 4. **Working on Your Changes**
Once your branch is created, you can start making changes. Keep the following best practices in mind:

- **Write clear and concise commit messages**. The message should explain *why* the change was made, not just *what* was changed. For example, "Fix login bug by correcting session timeout" is clearer than "Fix login bug."
- **Make small, focused commits**. Each commit should ideally implement one change or fix. This makes the review process easier and more manageable.
- **Test your changes**. Always test locally to make sure your changes work as expected and don’t break existing functionality.
  - If you're fixing a bug, ensure the bug is gone and write tests to confirm the fix.
  - If you're adding a feature, ensure the feature works and doesn't negatively affect the rest of the project.

## 5. **Keeping Your Branch Updated**
While you're working on your branch, it’s important to keep it up to date with the base branch (usually `main` or `develop`) to avoid merge conflicts later.

- **Sync your branch**: Periodically fetch and merge changes from the original repository to your branch.
  - You can add the original repository as an upstream remote (`git remote add upstream <url>`) and then run `git fetch upstream` followed by `git merge upstream/main` to keep your branch in sync.

## 6. **Creating a Pull Request**
Once your changes are complete and ready for review, create a pull request (PR) to propose merging your branch into the main repository.

- **Submit a pull request**:
  - Ensure your PR is pointing to the correct base branch (usually `main` or `develop`).
  - Add a clear and concise description to your PR explaining the changes you made, and link it to the corresponding issue.
  - Be sure to reference the issue by using keywords such as `fixes #123` or `closes #123` to automatically close the issue when the PR is merged.
  
- **Review process**:
  - A maintainer will review your PR and may request changes or provide feedback.
  - Be responsive and make any necessary updates to your code based on feedback.
  - **Do not squash commits** until the PR has been approved. This helps maintain a clear history of changes.

## 7. **Responding to Feedback**
Be open to constructive feedback. The review process is meant to ensure the project maintains a high-quality standard.

- **Address feedback**: If a reviewer asks for changes, be sure to respond and implement those changes promptly.
- **Make sure your changes are consistent with the project’s style and guidelines**, such as code formatting, naming conventions, and documentation standards.
- **Update tests**: If your PR affects functionality, make sure that tests are added or updated accordingly.

## 8. **Merging the Pull Request**
Once your PR has been reviewed and approved:

- **Merge the PR**: After approval, a maintainer will merge the pull request into the main branch.
- If you're instructed to **rebase** before merging (to maintain a clean history), do so carefully. Use `git rebase` followed by a `git push --force` to update the PR with a rebase.

## 9. **Post-Merge**
After your PR is merged:

- **Delete your branch**: Once the PR is merged, delete your branch both locally and on GitHub to keep the repository clean.
- **Sync your fork**: Don't forget to sync your fork with the main repository to reflect the new changes.
  
## 10. **Code of Conduct**
Be respectful, kind, and patient. This is an open-source project, and collaboration should always be positive and constructive.

- **Be respectful** to other contributors and maintainers.
- **Provide meaningful feedback**: If you’re reviewing someone else’s PR, provide thoughtful and actionable suggestions.
- **Assume good intentions**: Everyone contributes with the goal of improving the project.

## 11. **Additional Notes**
- **Automated Testing & CI**: Ensure that all tests pass before submitting your PR. All our repos will be delivered with some kind of test framework in the source (for example: dotnet test) will run the tests in our .NET projects.
- **Documentation**: If you’re adding or changing functionality, make sure to update the documentation accordingly. This can include README files, code comments, or dedicated markdown files. Our github workflows will generate the documentation websites automatically