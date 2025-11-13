export const env = {
  // Can be overridden with VITE_SIGNALR_HUB_URL later if you want
  signalRHubUrl: import.meta.env.VITE_SIGNALR_HUB_URL ?? '/ws/mda',
};