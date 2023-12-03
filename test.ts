
export interface JobHistory {
    job: JobData;
    logMessage: LogMessageData;
}

export type JobData = Array<{
    jobName: string;
    jobCode: string;
}>

export interface LogMessageData {
    message: string;
    messageType: string;
}


var listScores: Array<{ [key: string]: number }> = [];
const myarray = ['abc', 'abc', 'abc', 'abc', 'qwe', 'qwe', 'qwe', 'sss', 'sss', 'sss', 'sss', 'sss'];
myarray.forEach(x => listScores[x] = (listScores[x] ? ++listScores[x] : 1));

/// ( listScores[x] !== null ? ++listScores[x] : 1 )
/// dit is gewoon een if 
/// if (listScores[x] !== null) blablabla : blablablaVanElse



const student = {

    // data property
    firstName: 'Monica',

    // accessor property(getter)

    getters: {
        getSpeceficMessagetype: (state, messageType) => {
            return state + "hi" + messageType;
        }
    }

};
// var st = student.res(",","asd")
// console.log(st);
// getters: {
//     messageTypeFilter: (state) => {
//         const clonedObj = deepclone(...)
//         var listAllMessagesOfAllJobs: LogMessageData[] = [];
//         clonedObj.forEach(element => {
//             listAllMessagesOfAllJobs.push(element.LogMessages);
//         });
//         return (demandedMessageType) =>  listAllMessagesOfAllJobs.filter(x => x.messageType === demandedMessageType);  
//     },
// }