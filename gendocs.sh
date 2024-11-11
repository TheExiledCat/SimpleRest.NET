#do not run this script, just meant for github actions
cd docs
echo "Generating changelog"
export GIT_CHANGELOG_REMOTE=origin2
# echo $GIT_CHANGELOG_REMOTE;
git-changelog -io CHANGELOG.md 
doxygen Doxyfile
cd ..
