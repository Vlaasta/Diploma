let lastVisitedUrl = null;
let lastVisitedTitle = null;
let lastVisitStart = null;

// Завантаження збережених даних при запуску розширення
chrome.storage.local.get(["lastVisitedUrl", "lastVisitedTitle", "lastVisitStart"], function(result) {
  if (result.lastVisitedUrl && result.lastVisitStart) {
    lastVisitedUrl = result.lastVisitedUrl;
    lastVisitedTitle = result.lastVisitedTitle;
    lastVisitStart = new Date(result.lastVisitStart);
  }
});

// Обробник зміни активної вкладки
chrome.tabs.onActivated.addListener(async function(activeInfo) {
  const now = new Date();

  // Якщо є попередня вкладка, зберігаємо її дані
  if (lastVisitedUrl && lastVisitStart) {
    const timeSpent = Math.floor((now - lastVisitStart) / 1000);

    const data = {
      url: lastVisitedUrl,
      pageTitle: lastVisitedTitle || null,
      timestamp: lastVisitStart.toISOString(),
      timeSpent: timeSpent
    };

    sendDataToServer(data); // Відправка даних на сервер
  }

  // Оновлення даних для нової вкладки
  chrome.tabs.get(activeInfo.tabId, async function(tab) {
    if (!tab.url || !tab.url.startsWith("http")) return;

    lastVisitedUrl = tab.url;
    lastVisitedTitle = tab.title;
    lastVisitStart = now;

    // Зберігаємо нові дані в Chrome Storage
    chrome.storage.local.set({
      lastVisitedUrl: tab.url,
      lastVisitedTitle: tab.title,
      lastVisitStart: now.toISOString()
    });
  });
});

// Обробник зміни URL вкладки (наприклад, перехід на іншу сторінку)
chrome.tabs.onUpdated.addListener(function(tabId, changeInfo, tab) {
  if (changeInfo.status === "complete") {
    const now = new Date();

    // Якщо була попередня вкладка, зберігаємо її дані
    if (lastVisitedUrl && lastVisitStart) {
      const timeSpent = Math.floor((now - lastVisitStart) / 1000);

      const data = {
        url: lastVisitedUrl,
        pageTitle: lastVisitedTitle || null,
        timestamp: lastVisitStart.toISOString(),
        timeSpent: timeSpent
      };

      sendDataToServer(data); // Відправка даних на сервер
    }

    // Оновлення поточної вкладки
    if (!tab.url || !tab.url.startsWith("http")) return;

    lastVisitedUrl = tab.url;
    lastVisitedTitle = tab.title;
    lastVisitStart = now;

    // Зберігаємо нові дані в Chrome Storage
    chrome.storage.local.set({
      lastVisitedUrl: tab.url,
      lastVisitedTitle: tab.title,
      lastVisitStart: now.toISOString()
    });
  }
});

// Функція для відправки даних на сервер
function sendDataToServer(data) {
  fetch("http://localhost:5000/api/url/", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(data)
  })
  .then(response => {
    if (response.ok) {
      console.log("Дані успішно надіслано:", data);
    } else {
      console.error("Помилка при надсиланні даних:", response.statusText);
    }
  })
  .catch(error => {
    console.error("Помилка підключення до локального серверу:", error);
  });
}










