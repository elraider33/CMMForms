import { ApiService } from './api.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(
    private api: ApiService
  ) { }
  /**
   *
   * @param id represents the id of the parent record i.e. Bulletin or CMM
   */
  public downloadDocument(id: string) {
    return this.api.download(`Files?id=${id}`);
  }
  public downloadUrl(id: string) {
    return this.api.getDownloadUrl(`Files?id=${id}`);
  }
  public downloadDocumentAlternative(id: string) {
    return this.api.downloadAlternative(id);
  }
}
