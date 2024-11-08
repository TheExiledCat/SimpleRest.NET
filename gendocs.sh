cd docs
echo "Generating changelog"
git-changelog -io CHANGELOG.md
doxygen Doxyfile
cd ..
