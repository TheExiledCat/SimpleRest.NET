cd docs
echo "Generating changelog"
# export GIT_CHANGELOG_REMOTE=https://github.com/TheExiledCat/SimpleRest.NET.git 
# echo $GIT_CHANGELOG_REMOTE;
git-changelog -io CHANGELOG.md 
doxygen Doxyfile
cd ..
