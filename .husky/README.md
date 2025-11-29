# Husky Pre-commit Hooks

This directory contains Git hooks managed by Husky.

## Installed Hooks

### `commit-msg`

Validates commit messages using commitlint with conventional commit format.

**Format:** `type(scope): description`

**Examples:**

- `feat: add user authentication`
- `fix(api): resolve JWT token expiration bug`
- `docs: update README with setup instructions`

### `pre-commit`

Runs code quality checks on staged files before allowing commit:

1. **Prettier** - Formats TypeScript, JavaScript, JSON, Markdown, YAML files
2. **dotnet format** - Formats C# code and .csproj files
3. **ESLint** - Lints Angular applications (Portal and Admin)

## Usage

### First-time Setup

```bash
npm install
```

This runs `npm prepare` which initializes Husky hooks.

### Manual Formatting

```bash
# Format all files
npm run format

# Check formatting without changes
npm run format:check

# Lint Angular apps
npm run lint:portal
npm run lint:admin

# Check .NET formatting
npm run dotnet:format
```

### Bypassing Hooks (Use Sparingly)

```bash
git commit --no-verify -m "message"
```

## Configuration Files

- `.prettierrc` - Prettier formatting rules
- `.prettierignore` - Files/folders excluded from Prettier
- `.commitlintrc.json` - Commit message validation rules
- `.editorconfig` - Editor-agnostic code style settings
- `package.json` (lint-staged section) - Defines which tools run on which files

## How Pre-commit Works

1. You stage files with `git add`
2. You run `git commit`
3. Husky triggers `.husky/pre-commit`
4. `lint-staged` runs configured tools on staged files only
5. If all checks pass, commit proceeds
6. If any check fails, commit is aborted

## Troubleshooting

**Hook not running?**

```bash
npm run prepare
```

**Formatting conflicts?**
Run formatters manually before committing:

```bash
npm run format
dotnet format
```

**Need to commit without formatting?**

```bash
git commit --no-verify -m "WIP: unformatted code"
```
