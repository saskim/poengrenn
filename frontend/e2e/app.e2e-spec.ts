import { PoengrennPage } from './app.po';

describe('poengrenn App', () => {
  let page: PoengrennPage;

  beforeEach(() => {
    page = new PoengrennPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
