/** @type {import('ts-jest').JestConfigWithTsJest} */
module.exports = {
  preset: "ts-jest",
  testEnvironment: "jsdom",
  moduleNameMapper: {
    "^@/components/(.*)$": "<rootDir>/src/components/$1",
    "^@/features/(.*)$": "<rootDir>/src/features/$1",
    "^@/services/(.*)$": "<rootDir>/src/services/$1"
  },
  setupFilesAfterEnv: ["<rootDir>/jest.setup.ts"],
}