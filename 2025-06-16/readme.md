# 2025-06-16    Day - 31    Infinite Scroll, Dynamic Search, Routing

## Topics

- Dynamic Search (Search while type)

- Delay in dynamic search rate (Reducing No. of API calls)

- Infinite Scroll

- Routing

## Short Notes

Reducing the no. of API calls (in AJAX kinda search)

- Pipe (setting up a reactive pipeline to process search inputs)
    - Debounce - Waits (time) after the last emitted value before proceeding.
    - distinctUntilChanged - Only continues if the new search string is different from the last
    - switchMap - Cancels any previous request if a new search term is emitted, sends the latest search query
    - Tap


``` js
@HostListener("window:scroll")
fn()  // -> executes for every scroll action
```
## Links 
- https://chatgpt.com/share/685004cf-57d8-800a-8ff9-06955f76fc1d
- https://github.com/gayat19/PresidioMay25/blob/main/FE/Day1/myApp/src/app
- https://rxjs.dev/guide/overview
