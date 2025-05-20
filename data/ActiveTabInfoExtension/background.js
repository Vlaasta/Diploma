let lastVisitedUrl = null;
let lastVisitedTitle = null;
let lastVisitStart = null;

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

function sendDataToServer(data) {
  fetch("http://localhost:5000/api/url/", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data),
  })
    .then(res => {
      if (!res.ok) throw new Error(res.statusText);
      console.log("Успішно відправлено:", data);
    })
    .catch(err => {
      console.error("Помилка відправки чи CORS:", err);
    });
}

function handleNewPage(tabId, tabUrl, tabTitle) {
  const now = new Date();
  const localTimestamp = now.toISOString();

  if (lastVisitedUrl && lastVisitStart) {
    const timeSpent = Math.floor((now - lastVisitStart) / 1000);
    sendDataToServer({
      url: lastVisitedUrl,
      pageTitle: lastVisitedTitle,
      timestamp: localTimestamp,
      timeSpent,
      text: ""
    });
  }

  lastVisitedUrl = tabUrl;
  lastVisitedTitle = tabTitle;
  lastVisitStart = now;

  chrome.storage.local.set({
    lastVisitedUrl: tabUrl,
    lastVisitedTitle: tabTitle,
    lastVisitStart: now.toISOString() 
  });

  chrome.scripting.executeScript({
    target: { tabId },
    func: () => document.body.innerText.trim()
  }).then(results => {
    const pageText = (results[0] && results[0].result) || "";
    sendDataToServer({
      url: tabUrl,
      pageTitle: tabTitle,
      timestamp: localTimestamp, 
      timeSpent: 0,
      text: pageText
    });
  }).catch(err => {
    console.warn("Не вдалося інжектити скрипт для тексту:", err);
  });
}

chrome.tabs.onActivated.addListener(info => {
  chrome.tabs.get(info.tabId, tab => {
    if (tab.url && /^https?:/.test(tab.url)) {
      handleNewPage(info.tabId, tab.url, tab.title);
    }
  });
});

chrome.tabs.onUpdated.addListener((tabId, changeInfo, tab) => {
  if (
    changeInfo.status === "complete" &&
    tab.url &&
    /^https?:/.test(tab.url)
  ) {
    handleNewPage(tabId, tab.url, tab.title);
  }
});


















