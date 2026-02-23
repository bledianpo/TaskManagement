import { describe, it, expect } from "vitest";
import { normalizeStatus, normalizePriority, getStatusLabel } from "./task";

describe("normalizeStatus", () => {
  it("maps numeric status to TaskStatus", () => {
    expect(normalizeStatus(0)).toBe("Draft");
    expect(normalizeStatus(1)).toBe("InProgress");
    expect(normalizeStatus(2)).toBe("Completed");
  });

  it("returns Draft for null/undefined", () => {
    expect(normalizeStatus(null)).toBe("Draft");
    expect(normalizeStatus(undefined)).toBe("Draft");
  });
});

describe("normalizePriority", () => {
  it("maps numeric priority to TaskPriority", () => {
    expect(normalizePriority(0)).toBe("Low");
    expect(normalizePriority(1)).toBe("Medium");
    expect(normalizePriority(2)).toBe("High");
  });
});

describe("getStatusLabel", () => {
  it("returns display label for status", () => {
    expect(getStatusLabel(1)).toBe("In Progress");
    expect(getStatusLabel("Draft")).toBe("Draft");
  });
});
