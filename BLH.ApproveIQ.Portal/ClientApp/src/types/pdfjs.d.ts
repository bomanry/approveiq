// Fix for PDF.js TypeScript issues
declare global {
  interface OffscreenRenderingContext extends CanvasRenderingContext2D {}
}

export {};