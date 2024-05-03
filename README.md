# sync-github-release-to-gitee
自动同步github的release到gitee。

# 起步
* 需要在https://gitee.com/profile/personal_access_tokens新建一个token。
* 在github中添加secrets(https://github.com/{owner}/{repo}/settings/secrets/actions),名称为GITEE_TOKEN,值为上一步骤生成的token。
* 添加一个工作流模板sync_release_to_gitee.yml
```yml
name: sync_release_to_gitee

on:
  workflow_run:
    workflows: ["release"] #your release workflow name
    types: [completed]
      
jobs:
  sync2gitee:
    permissions: write-all
    runs-on: windows-latest 
    steps:
    - name: Checkout
      uses: actions/checkout@v4
      with:
        fetch-depth: 0

    - name: getExeFile
      run: (new-object System.Net.WebClient).DownloadFile('https://github.com/yibei333/sync-github-release-to-gitee/releases/download/1.0.0/SyncGithubReleaseToGitee.exe','./SyncGithubReleaseToGitee.exe')

    - name: sync
      env:
        gitee_token: ${{secrets.GITEE_TOKEN}}
        github_token: ${{secrets.GITHUB_TOKEN}}
        repo: ${{github.repository}}
      run: ./SyncGithubReleaseToGitee.exe
```
