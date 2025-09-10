import { Injectable } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class ODataService {

  public getOdataQuery(event: LazyLoadEvent, globalFilterFields: string[] = [], districtId?: string, schoolId?: string): string {
    let query = `$top=${event.rows}&skip=${event.first}`;

    if (event.filters) {
      let filterArray: string[] = [];

      for (let key of Object.keys(event.filters)) {
        if (key == "global" && event.filters[key].value) {
          let filterStr = '';
          globalFilterFields.forEach((element, index) => {
            if (index != 0) {
              filterStr += ' or ';
            }
            filterStr += `contains(${element},'${event.filters![key].value.replace(/'/g,"''")}')`
          });
          filterArray.push(`(${filterStr})`);
        } else if ((event.filters[key] as any[]).length > 0) {
          let filterStr = '';
          (event.filters[key] as any[]).forEach((element, index) => {
            if (element.value || element.value === false || element.value === 0) {
              let matchMode = element.matchMode;
              if (index != 0) {
                filterStr += ` ${element.operator} `;
              }
              switch (matchMode) {
                case "dateIs":
                  filterStr += `date(${key}) eq ${element.value.getFullYear()}-${element.value.getMonth() + 1}-${element.value.getDate()}`
                  return;
                case "dateIsNot":
                  filterStr += `date(${key}) ne ${element.value.getFullYear()}-${element.value.getMonth() + 1}-${element.value.getDate()}`
                  return;
                case "dateBefore":
                  filterStr += `date(${key}) lt ${element.value.getFullYear()}-${element.value.getMonth() + 1}-${element.value.getDate()}`
                  return;
                case "dateAfter":
                  filterStr += `date(${key}) gt ${element.value.getFullYear()}-${element.value.getMonth() + 1}-${element.value.getDate()}`
                  return;
                case "notContains":
                  matchMode = 'not contains';
                  break;
                case "equals":
                  if(typeof element.value === 'number')
                    filterStr += `${key} eq ${element.value}`
                  else
                    filterStr += `${key} eq '${element.value}'`
                  return;
                case "notEquals":
                  if(typeof element.value === 'number')
                    filterStr += `${key} ne ${element.value}`
                  else
                    filterStr += `${key} ne '${element.value}'`
                  return;
                case "lt":
                  if(typeof element.value === 'number')
                    filterStr += `${key} lt ${element.value}`
                  else
                    filterStr += `${key} lt '${element.value}'`
                  return;
                case "lte":
                  if(typeof element.value === 'number')
                    filterStr += `${key} le ${element.value}`
                  else
                    filterStr += `${key} le '${element.value}'`
                  return;
                case "gt":
                  if(typeof element.value === 'number')
                    filterStr += `${key} gt ${element.value}`
                  else
                    filterStr += `${key} gt '${element.value}'`
                  return;
                case "gte":
                  if(typeof element.value === 'number')
                    filterStr += `${key} ge ${element.value}`
                  else
                    filterStr += `${key} ge '${element.value}'`
                  return;
              }

              if(element.value === true || element.value === false){
                filterStr += `${key} eq ${element.value}`;
              } else {
                filterStr += `${matchMode}(${key},'${element.value}')`;
              }
            }
          });
          if (filterStr)
            filterArray.push(`(${filterStr})`);
        }
      }

      if (event.sortField) {
        if(Object.keys(event.sortField).length != 0)  query += `&orderBy=${event.sortField}`
        if (event.sortOrder == -1)
          query += " desc";
      }

      if (filterArray.length > 0) {
        query += "&filter=";
        filterArray.forEach((element, index) => {
          if (index != 0) {
            query += ' and ';
          }
          query += element;
        });
      }
    }

    return query;
  }
}
