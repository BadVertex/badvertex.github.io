---
layout: post
title: Console debugging in PHP
author: Håvard Kindem
---
Recently, I have been playing around in PHP again and found that I missed the simplicity of printing whatever object I wanted to the JavaScript console. Guess what? Now we all can!

After some fooling around, I ended up with the following class for handling it. Although it requires your code not to crash, I found it very useful when handling exceptions and inspecting objects when I set up a new API recently.

The attached archive includes both the source and example usage of the script. So sum it up, what you need to get started is this:

{% highlight php %}
<?php
    require_once("ConsoleDebug.php");
    use \Net\Nexcius\ConsoleDebug as Debug;

    Debug::LogD("This is a debug message");
?>
{% endhighlight %}

As always, feel free to use it however you like.

<!--more-->

{% highlight php %}
<?php
	namespace Net\Nexcius;

	/**
	* @author Håvard "Nexcius" Kindem
	*/
	class ConsoleDebug {
		const NONE = 0;
		const ERROR = 1;
		const WARN = 2;
		const INFO = 3;
		const DEBUG = 4;
		const LOG = 5;

		public static $LOG_LEVEL = self::LOG;

		/**
		* Prints the message to the console with the corresponding log level
		*/
		private static function PrintToConsole($level, $message) {
			switch ($level) {
				case self::ERROR:
					printf("<script>console.error(\"%s\");</script>", $message);
					return;

				case self::WARN:
					printf("<script>console.warn(\"%s\");</script>", $message);
					return;

				case self::INFO:
					printf("<script>console.info(\"%s\");</script>", $message);
					return;

				case self::DEBUG:
					printf("<script>console.debug(\"%s\");</script>", $message);
					return;
				
				default:
					printf("<script>console.log(\"%s\");</script>", $message);
					return;
			}
		}

		/**
		* Retrieves a textual representation of an object
		* @return A string representation of an object 
		*/
		private static function GetObjectContent($object) {
			$content = "";

			if(is_array($object)) {
				$content .= "{";
				for($i = 0; $i < sizeof($object); $i++) {
					if($i > 0) {
						$content .= ",";
					}

					$content .= self::GetObjectContent($object[$i]);
				}
				$content .= "}";
			}
			else if(is_object($object)) {
				ob_start();
				var_dump($object);
				$varDumpResult = ob_get_clean();

				$content .= html_entity_decode(str_replace("\n", "\\n", strip_tags($varDumpResult)));
			}
			else {
				$content .= $object;
			}

			return $content;
		}

		/**
		* Retrieves the callee's filename and line numbers and asks to print the message
		*/
		private static function LogMessage($level, $message) {
			$stackTrace = debug_backtrace();
			$file = addslashes($stackTrace[1]['file']);
			$line = $stackTrace[1]['line'];

			self::PrintToConsole($level, "{$file}:{$line}\\n" . $message);
		}

		/**
		* Prints a message to the console
		*/
		public static function Log($obj) {
			if(self::$LOG_LEVEL >= self::LOG) {
				self::LogMessage(self::LOG, self::GetObjectContent($obj));
			}
		}

		/**
		* Prints a debug message to the console
		*/
		public static function LogD($obj) {
			if(self::$LOG_LEVEL >= self::DEBUG) {
				self::LogMessage(self::DEBUG, self::GetObjectContent($obj));
			}
		}

		/**
		* Prints an info message to the console
		*/
		public static function LogI($obj) {
			if(self::$LOG_LEVEL >= self::INFO) {
				self::LogMessage(self::INFO, self::GetObjectContent($obj));
			}
		}

		/**
		* Prints a warning message to the console
		*/
		public static function LogW($obj) {
			if(self::$LOG_LEVEL >= self::WARN) {
				self::LogMessage(self::WARN, self::GetObjectContent($obj));
			}
		}

		/**
		* Prints an error message to the console
		*/
		public static function LogE($obj) {
			if(self::$LOG_LEVEL >= self::ERROR) {
				self::LogMessage(self::ERROR, self::GetObjectContent($obj));
			}
		}
	}
?>
{% endhighlight %}