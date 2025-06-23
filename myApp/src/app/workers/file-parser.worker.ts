/// <reference lib="webworker" />



addEventListener('message', ({ data }) => {
	const file: File = data.file;
	const reader = new FileReader();
	
	reader.onload = async () => {
		postMessage('Starting parsing...');
		await new Promise(() => setTimeout(()=>{},2000));
		postMessage(reader.result);
	};

	reader.onerror = () => {
		postMessage('ERROR: failed to read file');
	};

	reader.readAsText(file); 
});
