/*
Copyright 2026 Andreas Albang

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is 
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and limitations under the License.
*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedConnectPlugin.GUI
{
    //Warning shown when the configuration is loaded from the (untrusted) current working directory.
    //Offers a "Do not show this warning again" checkbox, so the user can disable it directly from the
    //dialog. Suppressing the warning is only safe when the working directory is protected by strict
    //file system permissions (see README).
    public class WorkingDirectoryWarningDialog : Form
    {
        private CheckBox checkBoxDoNotShowAgain;

        //True when the user ticked "Do not show this warning again".
        public Boolean DoNotShowAgain
        {
            get { return this.checkBoxDoNotShowAgain.Checked; }
        }

        public WorkingDirectoryWarningDialog(Icon dialogIcon, String configFilePath)
        {
            String message =
                "The Advanced Connect configuration was loaded from the current working directory instead of "
                + "the portable program directory (next to KeePass.exe) or your user profile (AppData):"
                + Environment.NewLine + Environment.NewLine
                + configFilePath
                + Environment.NewLine + Environment.NewLine
                + "This directory is determined by how KeePass was started (for example the folder of a "
                + "double-clicked database file) and may not be trusted. The configuration controls which "
                + "programs are launched and receives your entry credentials. "
                + Environment.NewLine + Environment.NewLine
                + "Only continue if you trust the origin of this file.";

            //Form
            this.Text = "Advanced Connect: configuration loaded from working directory";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            if (dialogIcon != null)
            {
                this.Icon = dialogIcon;
            }

            //Fixed layout metrics. The content width is fixed; the message and checkbox grow in height
            //(a long configuration path wraps over multiple lines), and the checkbox, button and the
            //form size are positioned relative to the measured text heights.
            const int margin = 12;
            const int contentLeft = 60;
            const int contentWidth = 412;
            const int spacing = 16;

            //Text shown in the checkbox. Keep the measurement (below) and the assignment in sync, so the
            //measured height matches the text that is actually rendered.
            const string checkBoxText = "Do not show this warning again";

            //Measure the wrapped text heights up-front. AutoSize controls only report their final height
            //once they have a handle/parent, which is too late for positioning here, so measure explicitly.
            Size measureBounds = new Size(contentWidth, Int32.MaxValue);
            const TextFormatFlags measureFlags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
            int messageHeight = TextRenderer.MeasureText(message, this.Font, measureBounds, measureFlags).Height;
            //The checkbox glyph needs some extra width; reserve it so its text wraps like it will be shown.
            int checkBoxTextWidth = contentWidth - 20;
            int checkBoxTextHeight = TextRenderer.MeasureText(
                checkBoxText,
                this.Font, new Size(checkBoxTextWidth, Int32.MaxValue), measureFlags).Height;
            int checkBoxHeight = Math.Max(checkBoxTextHeight, 17);

            //Warning icon
            PictureBox pictureBoxIcon = new PictureBox();
            pictureBoxIcon.Image = SystemIcons.Warning.ToBitmap();
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxIcon.Location = new Point(margin, margin);

            //Message (fixed width, explicit measured height, so a long path wraps and grows downwards)
            Label labelMessage = new Label();
            labelMessage.AutoSize = false;
            labelMessage.Location = new Point(contentLeft, margin);
            labelMessage.Size = new Size(contentWidth, messageHeight);
            labelMessage.Text = message;

            //Checkbox (placed below the message; height follows its wrapped text)
            this.checkBoxDoNotShowAgain = new CheckBox();
            this.checkBoxDoNotShowAgain.AutoSize = false;
            this.checkBoxDoNotShowAgain.Location = new Point(contentLeft, labelMessage.Bottom + spacing);
            this.checkBoxDoNotShowAgain.Size = new Size(contentWidth, checkBoxHeight);
            this.checkBoxDoNotShowAgain.Text = checkBoxText;

            //OK button (placed below the checkbox and right-aligned to the content area)
            Button buttonOk = new Button();
            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Size = new Size(75, 23);
            buttonOk.Location = new Point(
                contentLeft + contentWidth - buttonOk.Width,
                this.checkBoxDoNotShowAgain.Bottom + spacing);
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            //Size the form to fit all content (the height follows the wrapped message/checkbox).
            this.ClientSize = new Size(
                contentLeft + contentWidth + margin,
                buttonOk.Bottom + margin);

            this.Controls.Add(pictureBoxIcon);
            this.Controls.Add(labelMessage);
            this.Controls.Add(this.checkBoxDoNotShowAgain);
            this.Controls.Add(buttonOk);

            this.AcceptButton = buttonOk;
        }
    }
}
