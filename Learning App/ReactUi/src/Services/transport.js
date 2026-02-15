let requestId = 0;
const pending = new Map();

const DEFAULT_TIMEOUT = 10_000; // 10 שניות

function isValidResponse(data) {
    return (
        data &&
        typeof data.requestId === "number" &&
        typeof data.success === "boolean"
    );
}

window.chrome?.webview?.addEventListener("message", e => {


   
    if (!isValidResponse(e.data)) return;

    const { requestId, success, data, error } = e.data;
    const resolver = pending.get(requestId);
    if (!resolver) return;

    clearTimeout(resolver.timeoutId);

    success
        ? resolver.resolve(data)
        : resolver.reject(new Error(error || "Backend error"));

    pending.delete(requestId);
});

export function callBackend(action, payload = {}, timeout = DEFAULT_TIMEOUT) {
    if (!window.chrome?.webview) {
        return Promise.reject(
            new Error("WebView2 is not ready")
        );
    }

    return new Promise((resolve, reject) => {
        const id = ++requestId;

        const timeoutId = setTimeout(() => {
            pending.delete(id);
            reject(new Error(`Backend timeout: ${action}`));
        }, timeout);

        pending.set(id, { resolve, reject, timeoutId });

        window.chrome.webview.postMessage({
            requestId: id,
            action,
            payload
        });
    });
}
