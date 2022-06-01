import { HttpResponse } from '@angular/common/http';
import { Bulletin } from '../interfaces/bulletin.interface';
import { CMM } from '../interfaces/cmm.interface';
import { GroupBy } from '../interfaces/groupby.interface';
export const toFormData = (data) => {
    const formData = new FormData();
    const keys = Object.keys(data);
    for (let key of keys) {
        if(Array.isArray(data[key])){
          if(data[key].length){
            data[key].forEach(element => {
              formData.append(`${key}[]`, element);
            });
          }else{
            formData.append(`${key}`, '');
          }
        }
        else if(data[key] instanceof Date){
          formData.append(key, data[key].toJSON());
        }
        else{
            formData.append(key, data[key] ?? '');
        }
    }
    return formData;
}

export const downloadFile = (data: Blob, fileName: string) => {
  const contentType = 'application/pdf';
  const downloadedFile = new Blob([data], {type: contentType});
  const fileUrl = URL.createObjectURL(downloadedFile);
  // window.open(fileUrl);
  const aElement = document.createElement('a');
  aElement.setAttribute('style', 'display:none');
  document.body.appendChild(aElement);
  aElement.href = fileUrl;
  aElement.download = fileName;
  // aElement.target = '_blank';
  aElement.click();
  document.body.removeChild(aElement);
};

export function stringToArray(value) {
  return value ? !Array.isArray(value) ? value.split(/\r\n?/g) : value : null;
}

export function returnFilter(filter, fieldValue) {
  return filter !== '' ? fieldValue?.trim().toLowerCase().includes(filter) : true;
}

export function isBulletin(obj: any): obj is Bulletin {
  return 'sbno' in obj;
}

export function isCmm(obj: any): obj is CMM {
  return 'cmmNumber' in obj;
}

export function isGroupBy(obj: any): obj is GroupBy {
  return 'initial' in obj;
}
