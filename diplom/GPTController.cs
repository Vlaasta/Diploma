using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI; // Для WebDriverWait
using System; 

namespace diplom
{
    class GPTController
    {
        public GPTController()
        {
            // Ініціалізуємо Chrome WebDriver
            using (IWebDriver driver = new ChromeDriver())
            {
                // Відкриваємо сайт ChatGPT
                driver.Navigate().GoToUrl("https://chat.openai.com");

                // Явне очікування елемента (textarea з placeholder)
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20)); // Збільшено час очікування

                try
                {
                    // Очікуємо, поки елемент стане видимим та доступним для взаємодії
                    IWebElement inputField = wait.Until(d =>
                        d.FindElement(By.CssSelector("textarea[placeholder='Ask anything']")));

                    // Перевірка на видимість та доступність елемента
                    if (inputField.Displayed && inputField.Enabled)
                    {
                        // Вводимо запит
                        inputField.SendKeys("Ваш запит до ChatGPT");

                        // Можна натискати Enter, щоб відправити запит
                        inputField.SendKeys(Keys.Enter);
                    }
                    else
                    {
                        Console.WriteLine("Поле вводу недоступне для взаємодії.");
                    }
                }
                catch (ElementNotInteractableException ex)
                {
                    Console.WriteLine("Помилка: елемент не доступний для взаємодії.");
                }
                catch (NoSuchElementException ex)
                {
                    Console.WriteLine("Елемент не знайдений на сторінці.");
                }
            }
        }
    }
}


