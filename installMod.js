let fs = require('fs');

let from = './My/bin/Debug/net48/My.dll';
let to = 'C:/Users/user/Desktop/My.dll';

fs.copyFileSync(from, to);