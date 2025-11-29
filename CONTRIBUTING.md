# Contributing to Inventive

## Commit Message Convention

This project uses [Conventional Commits](https://www.conventionalcommits.org/) to maintain a clean and structured commit history.

### Format

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Type

Must be one of the following:

- **feat**: A new feature
- **fix**: A bug fix
- **docs**: Documentation only changes
- **style**: Changes that do not affect the meaning of the code (white-space, formatting, etc)
- **refactor**: A code change that neither fixes a bug nor adds a feature
- **perf**: A code change that improves performance
- **test**: Adding missing tests or correcting existing tests
- **build**: Changes that affect the build system or external dependencies
- **ci**: Changes to CI configuration files and scripts
- **chore**: Other changes that don't modify src or test files
- **revert**: Reverts a previous commit

### Scope (Optional)

The scope should be the name of the affected area:

- `api` - Customer API
- `admin-api` - Admin API
- `core` - Core domain models
- `services` - Business logic layer
- `data` - Data access layer
- `workers` - Background workers
- `test` - Testing

### Subject

- Use imperative, present tense: "change" not "changed" nor "changes"
- Don't capitalize the first letter
- No period (.) at the end
- Maximum 100 characters

### Examples

#### Good Commits ✅

```
feat(api): add JWT authentication for customer endpoints

Implements JWT bearer token authentication using Microsoft.AspNetCore.Authentication.JwtBearer.
Tokens expire after 1 hour and include customer ID claims.

Closes #42
```

```
fix(services): prevent double-booking with optimistic concurrency

Added timestamp-based concurrency check in ReservationService to prevent
race conditions when multiple users try to book the same equipment.
```

```
docs: update CLAUDE.md with Phase 1 completion criteria
```

```
refactor(data): simplify repository pattern for equipment queries
```

```
test(services): add unit tests for reservation conflict detection
```

#### Bad Commits ❌

```
Fixed bug
```
*Missing type, too vague*

```
FEAT: Added new feature
```
*Type should be lowercase*

```
feat(api): Added authentication.
```
*Subject should not be capitalized or end with a period*

```
updated stuff
```
*Missing type, too vague*

### Breaking Changes

If your commit introduces breaking changes, add `BREAKING CHANGE:` in the footer:

```
feat(api)!: change reservation API response format

BREAKING CHANGE: ReservationDto now uses ISO 8601 date format instead of Unix timestamps
```

Or use `!` after the type:

```
refactor(core)!: rename Equipment.Name to Equipment.DisplayName
```

### Automated Validation

This project uses commitlint with husky to automatically validate commit messages. If your commit message doesn't follow the convention, the commit will be rejected.

To test your commit message before committing:

```bash
echo "feat(api): add user registration" | npx commitlint
```

### Tips

1. **Keep commits atomic**: One logical change per commit
2. **Write meaningful subjects**: Someone should understand what changed without reading the diff
3. **Use the body for context**: Explain *why* the change was made, not *what* changed (that's what the diff is for)
4. **Reference issues**: Use `Closes #123` or `Fixes #456` in the footer

### Helpful Resources

- [Conventional Commits Specification](https://www.conventionalcommits.org/)
- [How to Write a Git Commit Message](https://chris.beams.io/posts/git-commit/)
- [Semantic Versioning](https://semver.org/)
