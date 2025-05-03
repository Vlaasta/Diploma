// background.js

let lastVisitedUrl = null;
let lastVisitedTitle = null;
let lastVisitStart = null;

// Завантажуємо попередній стан зі сховища
chrome.storage.local.get(
  ["lastVisitedUrl", "lastVisitedTitle", "lastVisitStart"],
  ({ lastVisitedUrl: url, lastVisitedTitle: title, lastVisitStart: ts }) => {
    if (url && ts) {
      lastVisitedUrl = url;
      lastVisitedTitle = title;
      lastVisitStart = new Date(ts);
    }
  }
);

// Допоміжка для POST
function sendDataToServer(data) {
  fetch("http://localhost:5000/api/url/", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  })
    .then(res => {
      if (!res.ok) throw new Error(res.statusText);
      console.log("✅ Успішно відправлено:", data);
    })
    .catch(err => {
      console.error("❌ Помилка відправки чи CORS:", err);
    });
}

// Обробляємо як активацію вкладки, так і оновлення URL/перезавантаження
function handleNewPage(tabId, tabUrl, tabTitle) {
  const now = new Date();

  // 1) Відправляємо дані про попередню сторінку (без тексту)
  if (lastVisitedUrl && lastVisitStart) {
    const timeSpent = Math.floor((now - lastVisitStart) / 1000);
    sendDataToServer({
      url: lastVisitedUrl,
      pageTitle: lastVisitedTitle,
      timestamp: lastVisitStart.toISOString(),
      timeSpent,
      text: ""
    });
  }

  // 2) Оновлюємо “стан” на поточну
  lastVisitedUrl = tabUrl;
  lastVisitedTitle = tabTitle;
  lastVisitStart = now;
  chrome.storage.local.set({
    lastVisitedUrl: tabUrl,
    lastVisitedTitle: tabTitle,
    lastVisitStart: now.toISOString()
  });

  // 3) Інжектимо скрипт у сторінку, щоб витягти її текст
  chrome.scripting.executeScript({
    target: { tabId },
    func: () => document.body.innerText.trim()
  }).then(results => {
    // results — масив з одним елементом
    const pageText = (results[0] && results[0].result) || "";
    console.log("🛰 Отримали текст довжиною", pageText.length);

    // 4) Відправляємо вже всі дані з текстом
    sendDataToServer({
      url: tabUrl,
      pageTitle: tabTitle,
      timestamp: now.toISOString(),
      timeSpent: 0,
      text: pageText
    });
  }).catch(err => {
    console.warn("Не вдалося інжектити скрипт для тексту:", err);
  });
}

//  --- Ловимо активацію вкладки ---
chrome.tabs.onActivated.addListener(info => {
  chrome.tabs.get(info.tabId, tab => {
    if (tab.url && /^https?:/.test(tab.url)) {
      handleNewPage(info.tabId, tab.url, tab.title);
    }
  });
});

//  --- Ловимо оновлення (перезавантаження / навігацію в тій самій вкладці) ---
chrome.tabs.onUpdated.addListener((tabId, changeInfo, tab) => {
  if (
    changeInfo.status === "complete" &&
    tab.url &&
    /^https?:/.test(tab.url)
  ) {
    handleNewPage(tabId, tab.url, tab.title);
  }
});


















