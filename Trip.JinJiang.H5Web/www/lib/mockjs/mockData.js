Mock.mock('http://g.cn', {
    'name': '@name()',
    'age|1-100': 100,
    'color': '@color'
});



// 使用 Mock
//var data = Mock.mock({
//    'list|1-10': [{
//        'id|+1': 1,
//        'count|1-100': 100,
//        'decuceAmount|1-100': 100,
//        'endDate': '@DATETIME()',
//        'startDate': '@DATETIME()',
//        'source':/\w\W\s\S\d\D/
//    }]
//});
//  debugger