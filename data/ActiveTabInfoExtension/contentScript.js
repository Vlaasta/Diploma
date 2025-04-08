chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
  if (request.action === "getHTML") {
    // Отримуємо HTML-код усієї сторінки
    const htmlContent = document.documentElement.outerHTML;
    sendResponse({ html: htmlContent });
  }
});