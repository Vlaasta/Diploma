chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
  if (request.action === "getHTML") {
    const htmlContent = document.documentElement.outerHTML;
    sendResponse({ html: htmlContent });
  }
});