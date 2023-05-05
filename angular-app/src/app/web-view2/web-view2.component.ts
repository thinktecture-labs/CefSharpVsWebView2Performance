import { AfterViewInit, Component } from '@angular/core';
import { SampleDto, generateSampleDtos } from '../sample-dto';

declare global {
  interface Window {
    chrome: Chrome;
  }

  interface Chrome {
    webview: WebView;
  }

  interface WebView {
    postMessage(message: WebMessage): void;
    addEventListener(type: string, listener: (event: WebViewEvent) => void, options?: boolean | AddEventListenerOptions): void;
  }

  interface WebViewEvent {
    data: any
  }
}

interface WebMessage {
  type: string;
  simpleResult?: number;
  complexResult?: SampleDto[]
}

@Component({
  selector: 'app-web-view2',
  templateUrl: './web-view2.component.html'
})
export class WebView2Component implements AfterViewInit {
  ngAfterViewInit(): void {
    if (typeof window.chrome === 'undefined' || typeof window.chrome.webview === 'undefined')
      return;

    window.chrome.webview.addEventListener('message', event => {
      if (event.data === 'simple')
        this.simpleCall();
      else
        this.complexCall();
    });
  }

  simpleCall() {
    window.chrome.webview.postMessage({ type: 'simple', simpleResult: 42 });
  }

  complexCall() {
    window.chrome.webview.postMessage({ type: 'complex', complexResult: generateSampleDtos() });
  }
}