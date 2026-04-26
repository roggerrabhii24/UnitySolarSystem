using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class enables output to in-game onscreen console. Provides static access
/// via singleton. Text can be formatted and colored.
/// </summary>
public class ConsoleOutput : Singleton<ConsoleOutput> {

    private Text[] _textFields;
    private int _maxNumberOfMessages;
    private int _numberOfMessages = 0;

    private void Awake()
    {
        _textFields = GetComponentsInChildren<Text>();
        _maxNumberOfMessages = _textFields.Length;
    }

    /// <summary>
    /// Posts a message to the console output which is then displayed on the UI.
    /// </summary>
    /// <param name="message">Message text</param>
    /// <param name="color">Message color</param>
    public void PostMessage(string message, Color color)
    {
        if(_numberOfMessages < _maxNumberOfMessages)
        {
            _numberOfMessages++;
            for (int i = _numberOfMessages - 1; i > 0; i--)
            {
                _textFields[i].text = _textFields[i - 1].text;
                _textFields[i].color = _textFields[i - 1].color;
            }
            _textFields[0].text = message;
            _textFields[0].color = color;
        }
        else
        {
            // Circular buffer imitation
            for (int i = _maxNumberOfMessages-1; i > 0; i--)
            {
                _textFields[i].text = _textFields[i-1].text;
                _textFields[i].color = _textFields[i - 1].color;
            }
            _textFields[0].text = message;
            _textFields[0].color = color;
        }

        UpdateTextFieldAlpha();
    }

    /// <summary>
    /// Posts a message to the console output which is then displayed on the UI.
    /// Message color is white by default.
    /// </summary>
    /// <param name="message">Message text</param>
    public void PostMessage(string message)
    {
        PostMessage(message, Color.white);
    }

    /// <summary>
    /// Updates the text field alpha channel so that the oldest message fades the most.
    /// </summary>
    private void UpdateTextFieldAlpha()
    {
        Color textColor; 

        for (int i = 0; i < _maxNumberOfMessages; i++)
        {
            textColor = _textFields[i].color;

            textColor.a = (_maxNumberOfMessages - i + 1.0f) / (float) _maxNumberOfMessages;

            _textFields[i].color = textColor;
        }
    }

}
