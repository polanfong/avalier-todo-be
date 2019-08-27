
```bash
az acr task create \
    --registry avalier \
    --name task-build-avalier-todo-be \
    --image todo-be:{{.Run.ID}} \
    --context https://github.com/avalier/avalier-todo-be.git \
    --branch master \
    --file Dockerfile \
    --git-access-token $GIT_PAT
```