//*****************************************************************************
//** 2096. Step-By-Step Directions From a Binary Tree Node to Another        **
//** leetcode                                                       -Dan     **
//*****************************************************************************

/**
 * Definition for a binary tree node.
 * struct TreeNode {
 *     int val;
 *     struct TreeNode *left;
 *     struct TreeNode *right;
 * };
 */
// Helper function to find the path from root to a given node.
// Returns 1 if the path is found, otherwise 0.
int findPath(struct TreeNode *root, int val, char *path)
{
    if (root == NULL)
    {
        return 0;
    }
    if (root->val == val)
    {
        return 1;
    }

    // Search in the left subtree.
    if (findPath(root->left, val, path + 1))
    {
        *path = 'L';
        return 1;
    }

    // Search in the right subtree.
    if (findPath(root->right, val, path + 1))
    {
        *path = 'R';
        return 1;
    }

    return 0;
}

// Helper function to find the LCA of start and dest nodes.
struct TreeNode* findLCA(struct TreeNode* root, int startValue, int destValue)
{
    if (root == NULL || root->val == startValue || root->val == destValue)
    {
        return root;
    }

    struct TreeNode *leftLCA = findLCA(root->left, startValue, destValue);
    struct TreeNode *rightLCA = findLCA(root->right, startValue, destValue);

    if (leftLCA != NULL && rightLCA != NULL)
    {
        return root;
    }

    return (leftLCA != NULL) ? leftLCA : rightLCA;
}

char* getDirections(struct TreeNode* root, int startValue, int destValue)
{
    struct TreeNode *lca = findLCA(root, startValue, destValue);

    // Arrays to hold the paths from LCA to startValue and destValue.
    char startPath[100000] = {0}, destPath[100000] = {0};
    findPath(lca, startValue, startPath);
    findPath(lca, destValue, destPath);

    // Count the 'U's required to move up from startValue to the LCA.
    int upSteps = strlen(startPath);

    // Allocate memory for the result.
    char *finalPath = (char *)malloc((upSteps + strlen(destPath) + 1) * sizeof(char));

    // Fill in the 'U's for moving up to the LCA.
    for (int i = 0; i < upSteps; i++)
    {
        finalPath[i] = 'U';
    }

    // Append the path from LCA to destValue.
    strcpy(finalPath + upSteps, destPath);

    return finalPath;
}