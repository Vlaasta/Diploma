// contentScript.js

// Якщо ви підключили readability.js через manifest, тут Readability вже вікно.
chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
  if (request.action !== "getText") return;

  console.log("[CS] getText – запускаємо Readability…");

  // 1) Клон документа для Readability
  const docClone = document.cloneNode(true);

  let article, text = "";
  try {
    article = new Readability(docClone).parse();
  } catch (e) {
    console.error("[CS] Readability упав:", e);
  }

  if (article && article.textContent) {
    text = article.textContent.trim();
    console.log("[CS] Readability дав текст довжиною", text.length);
  } else {
    // fallback на весь текст сторінки
    text = document.body.innerText.trim();
    console.log("[CS] fallback body.innerText довжина", text.length);
  }

  sendResponse({ text });
  // без return true — порт закриється раніше, ніж sendResponse спрацює
  return true;
});








