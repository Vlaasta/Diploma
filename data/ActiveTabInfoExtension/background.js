// Обробка кліку по іконці розширення
chrome.action.onClicked.addListener((tab) => {
  console.log("Кнопка натиснута (v3)");
  // Отримання активної вкладки у поточному вікні
  chrome.tabs.query({ active: true, currentWindow: true }, function(tabs) {
    var activeTab = tabs[0];

    // Посилаємо повідомлення до content script, щоб отримати HTML-код
    chrome.tabs.sendMessage(activeTab.id, { action: "getHTML" }, function(response) {
      if (chrome.runtime.lastError) {
        console.error("Помилка отримання HTML:", chrome.runtime.lastError);
      } else {
        console.log("URL: " + activeTab.url);
        console.log("HTML-код сторінки: ", response.html);

        // Тут можна викликати функцію для передачі даних у WinForms (наприклад, через Native Messaging)
      }
    });
  });
});