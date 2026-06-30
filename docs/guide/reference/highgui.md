# HIGHGUI Module API Reference

Complete documentation for the **HighGUI** (High-level GUI) static methods and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Highgui Documentation](https://docs.opencv.org/5.x/main_modules/highgui.html).

---
<div v-pre>

## ⚙️ Static Methods (Cv2)

### `Cv2.NamedWindow`
**Signature**: `void NamedWindow(string winname, int flags)`

Creates a window.

**Detailed Remarks**:
The function creates a window that can be used as a placeholder for images and trackbars. Created windows are referred to by their names.
If a window with the same name already exists, the function does nothing.
You can call `Cv2.DestroyWindow` or `Cv2.DestroyAllWindows` to close the window and de-allocate any associated memory usage.

**Note:** Qt backend supports additional flags:
-   **WINDOW_NORMAL or WINDOW_AUTOSIZE:** `WINDOW_NORMAL` enables you to resize the window, whereas `WINDOW_AUTOSIZE` adjusts automatically the window size to fit the displayed image (see `Imshow`), and you cannot change the window size manually.
-   **WINDOW_FREERATIO or WINDOW_KEEPRATIO:** `WINDOW_FREERATIO` adjusts the image with no respect to its ratio, whereas `WINDOW_KEEPRATIO` keeps the image ratio.
-   **WINDOW_GUI_NORMAL or WINDOW_GUI_EXPANDED:** `WINDOW_GUI_NORMAL` is the old way to draw the window without statusbar and toolbar, whereas `WINDOW_GUI_EXPANDED` is the new enhanced GUI. By default, `flags == WINDOW_AUTOSIZE | WINDOW_KEEPRATIO | WINDOW_GUI_EXPANDED`.

**Parameters**:
* `winname`: Name of the window in the window caption that may be used as a window identifier.
* `flags`: Flags of the window. Use values from `WindowFlags`.

---
### `Cv2.DestroyWindow`
**Signature**: `void DestroyWindow(string winname)`

Destroys the specified window.

**Parameters**:
* `winname`: Name of the window to be destroyed.

---
### `Cv2.DestroyAllWindows`
**Signature**: `void DestroyAllWindows()`

Destroys all of the HighGUI windows.

---
### `Cv2.CurrentUIFramework`
**Signature**: `string? CurrentUIFramework()`

Returns the HighGUI backend name used (e.g., COCOA, GTK2/3, QT, WAYLAND, or WIN32).

**Returns**: The backend name string, or `null` / empty string if no UI backend is available.

---
### `Cv2.StartWindowThread`
**Signature**: `int StartWindowThread()`

Starts the window thread (used internally on some platforms).

**Returns**: An integer status code.

---
### `Cv2.WaitKeyEx`
**Signature**: `int WaitKeyEx(int delay)`

Similar to `WaitKey`, but returns the full key code.

**Note:** Key code is implementation-specific and depends on the backend used (QT/GTK/Win32/etc).

**Parameters**:
* `delay`: Delay in milliseconds.

**Returns**: The code of the pressed key or -1 if no key was pressed before the specified time elapsed.

---
### `Cv2.WaitKey`
**Signature**: `int WaitKey(int delay)`

Waits for a pressed key.

**Detailed Remarks**:
The function waits for a key event infinitely (when `delay ≤ 0`) or for `delay` milliseconds, when it is positive. It returns the code of the pressed key or -1 if no key was pressed before the specified time had elapsed. To check for a key press but not wait for it, use `PollKey`.

**Note:** The functions `WaitKey` and `PollKey` are the only methods in HighGUI that can fetch and handle GUI events, so one of them needs to be called periodically for normal event processing unless HighGUI is used within an environment that takes care of event processing.

**Note:** The function only works if there is at least one HighGUI window created and the window is active.

**Parameters**:
* `delay`: Delay in milliseconds. 0 is the special value that means "forever".

**Returns**: The code of the pressed key or -1 if no key was pressed.

---
### `Cv2.PollKey`
**Signature**: `int PollKey()`

Polls for a pressed key without waiting.

**Detailed Remarks**:
The function polls for a key event without waiting. It returns the code of the pressed key or -1 if no key was pressed since the last invocation. To wait until a key was pressed, use `WaitKey`.

**Returns**: The code of the pressed key or -1 if no key was pressed.

---
### `Cv2.Imshow`
**Signature**: `void Imshow(string winname, Mat mat)`

Displays an image in the specified window.

**Detailed Remarks**:
The function displays an image in the specified window. If the window was created with `WINDOW_AUTOSIZE`, the image is shown with its original size (limited by screen resolution). Otherwise, the image is scaled to fit the window.

Image depth handling:
-   **8-bit unsigned**: displayed as is.
-   **16-bit unsigned**: pixels are divided by 256. The value range [0, 255×256] maps to [0, 255].
-   **32-bit or 64-bit floating-point**: pixel values are multiplied by 255. The value range [0, 1] maps to [0, 255].
-   **32-bit integer**: not processed; convert to 8-bit unsigned manually.

If the window was not created before this function, it is assumed creating a window with `WINDOW_AUTOSIZE`.

**Note:** This function should be followed by a call to `WaitKey` or `PollKey` to perform GUI housekeeping tasks necessary to actually show the image. For example, `WaitKey(0)` will display the window infinitely until any keypress. `WaitKey(25)` is suitable for displaying a video frame-by-frame.

**Parameters**:
* `winname`: Name of the window.
* `mat`: Image to be shown.

---
### `Cv2.ResizeWindow`
**Signature**: `void ResizeWindow(string winname, int width, int height)`

Resizes the window to the specified size.

**Note:** The specified window size is for the image area. Toolbars are not counted. Only windows created without `WINDOW_AUTOSIZE` flag can be resized.

**Parameters**:
* `winname`: Window name.
* `width`: The new window width.
* `height`: The new window height.

---
### `Cv2.ResizeWindow` (overload)
**Signature**: `void ResizeWindow(string winname, Size size)`

Resizes the window to the specified size.

**Parameters**:
* `winname`: Window name.
* `size`: The new window size.

---
### `Cv2.MoveWindow`
**Signature**: `void MoveWindow(string winname, int x, int y)`

Moves the window to the specified position.

**Note:** [Wayland Backend Only] This function is not supported by the Wayland protocol limitation.

**Parameters**:
* `winname`: Name of the window.
* `x`: The new x-coordinate of the window.
* `y`: The new y-coordinate of the window.

---
### `Cv2.SetWindowProperty`
**Signature**: `void SetWindowProperty(string winname, int prop_id, double prop_value)`

Changes parameters of a window dynamically.

**Note:** [Wayland Backend Only] This function is not supported.

**Parameters**:
* `winname`: Name of the window.
* `prop_id`: Window property to edit. Use values from `WindowPropertyFlags`.
* `prop_value`: New value of the window property.

---
### `Cv2.SetWindowTitle`
**Signature**: `void SetWindowTitle(string winname, string title)`

Updates the window title.

**Parameters**:
* `winname`: Name of the window.
* `title`: New title.

---
### `Cv2.GetWindowProperty`
**Signature**: `double GetWindowProperty(string winname, int prop_id)`

Provides parameters of a window.

**Note:** [Wayland Backend Only] This function is not supported.

**Parameters**:
* `winname`: Name of the window.
* `prop_id`: Window property to retrieve. Use values from `WindowPropertyFlags`.

**Returns**: The current value of the window property.

---
### `Cv2.GetWindowImageRect`
**Signature**: `Rect GetWindowImageRect(string winname)`

Provides the rectangle of the image rendering area in the window.

**Note:** [Wayland Backend Only] This function is not supported by the Wayland protocol limitation.

**Parameters**:
* `winname`: Name of the window.

**Returns**: The client screen coordinates, width and height of the image rendering area.

---
### `Cv2.SelectROI`
**Signature**: `Rect SelectROI(string windowName, Mat img, bool showCrosshair, bool fromCenter, bool printNotice)`

Allows users to select a ROI on the given image.

**Detailed Remarks**:
The function creates a window and allows users to select a ROI using the mouse.
Controls: use `space` or `enter` to finish selection, use key `c` to cancel selection (function will return an empty `Rect`).

**Parameters**:
* `windowName`: Name of the window where the selection process will be shown.
* `img`: Image to select a ROI.
* `showCrosshair`: If `true`, crosshair of selection rectangle will be shown.
* `fromCenter`: If `true`, center of selection will match initial mouse position.
* `printNotice`: If `true`, a notice to select ROI or cancel selection will be printed in console.

**Returns**: Selected ROI or empty rect if selection was canceled.

---
### `Cv2.SelectROI` (overload)
**Signature**: `Rect SelectROI(Mat img, bool showCrosshair, bool fromCenter, bool printNotice)`

Allows users to select a ROI on the given image (without specifying a window name).

**Parameters**:
* `img`: Input image.
* `showCrosshair`: If `true`, crosshair of selection rectangle will be shown.
* `fromCenter`: If `true`, center of selection will match initial mouse position.
* `printNotice`: If `true`, a notice will be printed in console.

**Returns**: Selected ROI or empty rect if selection was canceled.

---
### `Cv2.SelectROIs`
**Signature**: `void SelectROIs(string windowName, Mat img, IntPtr boundingBoxes, bool showCrosshair, bool fromCenter, bool printNotice)`

Allows users to select multiple ROIs on the given image.

**Detailed Remarks**:
The function creates a window and allows users to select multiple ROIs using the mouse.
Controls: use `space` or `enter` to finish current selection and start a new one, use `esc` to terminate multiple ROI selection process.

**Parameters**:
* `windowName`: Name of the window where the selection process will be shown.
* `img`: Image to select a ROI.
* `boundingBoxes`: Selected ROIs (output).
* `showCrosshair`: If `true`, crosshair of selection rectangle will be shown.
* `fromCenter`: If `true`, center of selection will match initial mouse position.
* `printNotice`: If `true`, a notice will be printed in console.

---
### `Cv2.GetTrackbarPos`
**Signature**: `int GetTrackbarPos(string trackbarname, string winname)`

Returns the trackbar position.

**Note:** [Qt Backend Only] `winname` can be empty if the trackbar is attached to the control panel.

**Parameters**:
* `trackbarname`: Name of the trackbar.
* `winname`: Name of the window that is the parent of the trackbar.

**Returns**: The current position of the specified trackbar.

---
### `Cv2.SetTrackbarPos`
**Signature**: `void SetTrackbarPos(string trackbarname, string winname, int pos)`

Sets the trackbar position.

**Note:** [Qt Backend Only] `winname` can be empty if the trackbar is attached to the control panel.

**Parameters**:
* `trackbarname`: Name of the trackbar.
* `winname`: Name of the window that is the parent of the trackbar.
* `pos`: New position.

---
### `Cv2.SetTrackbarMax`
**Signature**: `void SetTrackbarMax(string trackbarname, string winname, int maxval)`

Sets the trackbar maximum position.

**Note:** [Qt Backend Only] `winname` can be empty if the trackbar is attached to the control panel.

**Parameters**:
* `trackbarname`: Name of the trackbar.
* `winname`: Name of the window that is the parent of the trackbar.
* `maxval`: New maximum position.

---
### `Cv2.SetTrackbarMin`
**Signature**: `void SetTrackbarMin(string trackbarname, string winname, int minval)`

Sets the trackbar minimum position.

**Note:** [Qt Backend Only] `winname` can be empty if the trackbar is attached to the control panel.

**Parameters**:
* `trackbarname`: Name of the trackbar.
* `winname`: Name of the window that is the parent of the trackbar.
* `minval`: New minimum position.

---
### `Cv2.AddText`
**Signature**: `void AddText(Mat img, string text, Point org, string nameFont, int pointSize, Scalar color, int weight, int style, int spacing)`

Draws text on the image (Qt backend only).

**Parameters**:
* `img`: 8-bit 3-channel image where the text should be drawn.
* `text`: Text to write on an image.
* `org`: Point(x,y) where the text should start on an image.
* `nameFont`: Name of the font. The name should match the name of a system font (such as *Times*). If not found, a default one is used.
* `pointSize`: Size of the font. If not specified, equal zero or negative, the point size is set to a system-dependent default value (generally 12 points).
* `color`: Color of the font in BGRA where A = 255 is fully transparent.
* `weight`: Font weight. Use values from `QtFontWeights` or specify a positive integer.
* `style`: Font style. Use values from `QtFontStyles`.
* `spacing`: Spacing between characters. It can be negative or positive.

---
### `Cv2.DisplayOverlay`
**Signature**: `void DisplayOverlay(string winname, string text, int delayms)`

Displays a text on a window image as an overlay for a specified duration.

**Detailed Remarks**:
The function displays useful information/tips on top of the window for a certain amount of time `delayms`. The function does not modify the image, displayed in the window — after the specified delay, the original content of the window is restored.

**Parameters**:
* `winname`: Name of the window.
* `text`: Overlay text to write on a window image.
* `delayms`: The period (in milliseconds) during which the overlay text is displayed. If this value is zero, the text never disappears.

---
### `Cv2.DisplayStatusBar`
**Signature**: `void DisplayStatusBar(string winname, string text, int delayms)`

Displays a text on the window statusbar during the specified period of time.

**Detailed Remarks**:
The function displays useful information/tips on the window statusbar for a certain amount of time `delayms`. The window must be created with the `WINDOW_GUI_EXPANDED` flag.

**Parameters**:
* `winname`: Name of the window.
* `text`: Text to write on the window statusbar.
* `delayms`: Duration (in milliseconds) to display the text. If this value is zero, the text never disappears.

---

## 🔢 Enumerations

### `MouseEventFlags`
Flags for mouse events indicating which buttons/modifiers are held during a mouse event.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Lbutton`** | `1` | Left button is held |
| **`Rbutton`** | `2` | Right button is held |
| **`Mbutton`** | `4` | Middle button is held |
| **`Ctrlkey`** | `8` | Ctrl key is held |
| **`Shiftkey`** | `16` | Shift key is held |
| **`Altkey`** | `32` | Alt key is held |

---
### `MouseEventTypes`
Types of mouse events.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Mousemove`** | `0` | Mouse moved |
| **`Lbuttondown`** | `1` | Left button pressed |
| **`Rbuttondown`** | `2` | Right button pressed |
| **`Mbuttondown`** | `3` | Middle button pressed |
| **`Lbuttonup`** | `4` | Left button released |
| **`Rbuttonup`** | `5` | Right button released |
| **`Mbuttonup`** | `6` | Middle button released |
| **`Lbuttondblclk`** | `7` | Left button double-clicked |
| **`Rbuttondblclk`** | `8` | Right button double-clicked |
| **`Mbuttondblclk`** | `9` | Middle button double-clicked |
| **`Mousewheel`** | `10` | Mouse wheel scrolled (vertical) |
| **`Mousehwheel`** | `11` | Mouse wheel scrolled (horizontal) |

---
### `QtButtonTypes`
Types of buttons available for Qt backend.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`PushButton`** | `0` | Push button |
| **`Checkbox`** | `1` | Checkbox |
| **`Radiobox`** | `2` | Radio button |
| **`NewButtonbar`** | `1024` | New button bar |

---
### `QtFontStyles`
Font styles for Qt backend text rendering.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Normal`** | `0` | Normal style |
| **`Italic`** | `1` | Italic style |
| **`Oblique`** | `2` | Oblique style |

---
### `QtFontWeights`
Font weights for Qt backend text rendering.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Light`** | `25` | Light weight |
| **`Normal`** | `50` | Normal weight |
| **`Demibold`** | `63` | Demibold weight |
| **`Bold`** | `75` | Bold weight |
| **`Black`** | `87` | Black (heaviest) weight |

---
### `WindowFlags`
Flags for creating or configuring windows.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Normal`** | `0x00000000` | The user can resize the window (no constraint); also used to switch a fullscreen window to a normal size |
| **`Autosize`** | `0x00000001` | The user cannot resize the window; the size is constrained by the displayed image |
| **`Opengl`** | `0x00001000` | Window with OpenGL support |
| **`Fullscreen`** | `1` | Change the window to fullscreen |
| **`Freeratio`** | `0x00000100` | The image expands as much as it can (no ratio constraint) |
| **`Keepratio`** | `0x00000000` | The ratio of the image is respected |
| **`GuiExpanded`** | `0x00000000` | Status bar and tool bar (new enhanced GUI) |
| **`GuiNormal`** | `0x00000010` | Old-fashioned way without statusbar and toolbar |

---
### `WindowPropertyFlags`
Flags for window properties (get/set operations).

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Fullscreen`** | `0` | Fullscreen property |
| **`Autosize`** | `1` | Autosize property |
| **`AspectRatio`** | `2` | Window aspect ratio property |
| **`Opengl`** | `3` | OpenGL support property |
| **`Visible`** | `4` | Visibility property |
| **`Topmost`** | `5` | Window topmost property |
| **`Vsync`** | `6` | VSync property |

</div>
