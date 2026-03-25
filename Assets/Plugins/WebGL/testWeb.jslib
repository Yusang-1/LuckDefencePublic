mergeInto(LibraryManager.library, {
  
    HelloString: function (str) {
        window.alert(UTF8ToString(str)); // 올바른 문자열 변환
    },
    
    Hello: function () {
    window.alert("Hello, world!");
  },
  
});