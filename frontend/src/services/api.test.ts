import { describe, it, expect } from "vitest";
import { getApiErrorMessage } from "./api";

describe("getApiErrorMessage", () => {
  it("returns message from response.data when present", () => {
    const error = {
      response: { data: { message: "Invalid email or password" } },
    };
    expect(getApiErrorMessage(error, "Login failed")).toBe(
      "Invalid email or password"
    );
  });

  it("returns fallback when response.data.message is missing", () => {
    const error = { response: { data: {} } };
    expect(getApiErrorMessage(error, "Login failed")).toBe("Login failed");
  });

  it("returns Error.message when no response and error is Error", () => {
    expect(getApiErrorMessage(new Error("Network error"), "Fallback")).toBe(
      "Network error"
    );
  });
});
