module.exports = {
  extends: [
    'eslint:recommended',
    'plugin:react/recommended',
    'plugin:@typescript-eslint/recommended'
  ],
  parser: '@typescript-eslint/parser',
    parserOptions: {
      ecmaFeatures: {
      "jsx": true
    },
      ecmaVersion: 12,
      sourceType: "module"
  },
  plugins: [
    'react',
    '@typescript-eslint'
  ],
  rules: {
    "@typescript-eslint/no-explicit-any": "off",
    "react/react-in-jsx-scope": "off",
    "@typescript-eslint/no-unused-vars": ["error", { "varsIgnorePattern": "^React$" }]
  },
  settings: {
    react: {
      version: 'detect',
    },
  },
};