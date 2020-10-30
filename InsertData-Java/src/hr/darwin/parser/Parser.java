package hr.darwin.parser;

import hr.darwin.model.Person;
import hr.darwin.model.Vehicle;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Parser {

    private static final char DEFAULT_SEPARATOR = ';';
    private static final char DEFAULT_QUOTE = '"';

    public static <T> List<T> parseCSV(String file, T type) throws FileNotFoundException {
        List<T> parseData = new ArrayList<>();
        Scanner scanner = new Scanner(new File(file));

        if (scanner.hasNext()) {
            scanner.nextLine();
        }

        while (scanner.hasNext()) {
            List<String> line = parseLine(scanner.nextLine());

            if (type instanceof Person) {
                parseData.add((T) new Person(line.get(0), line.get(1), line.get(2), line.get(3)));
            }

            if (type instanceof Vehicle) {
                parseData.add((T) new Vehicle(Integer.parseInt(line.get(0)), Integer.parseInt(line.get(1)), line.get(2), line.get(3)));
            }
        }

        scanner.close();
        return parseData;
    }

    private static List<String> parseLine(String cvsLine) {
        return parseLine(cvsLine, DEFAULT_SEPARATOR, DEFAULT_QUOTE);
    }

    private static List<String> parseLine(String cvsLine, char separators, char customQuote) {

        List<String> result = new ArrayList<>();
        if (cvsLine == null && cvsLine.isEmpty()) {
            return result;
        }

        if (customQuote == ' ') {
            customQuote = DEFAULT_QUOTE;
        }

        if (separators == ' ') {
            separators = DEFAULT_SEPARATOR;
        }

        StringBuffer curVal = new StringBuffer();
        boolean inQuotes = false;
        boolean startCollectChar = false;
        boolean doubleQuotesInColumn = false;

        char[] chars = cvsLine.toCharArray();

        for (char ch : chars) {

            if (inQuotes) {
                startCollectChar = true;
                if (ch == customQuote) {
                    inQuotes = false;
                    doubleQuotesInColumn = false;
                } else {

                    if (ch == '\"') {
                        if (!doubleQuotesInColumn) {
                            curVal.append(ch);
                            doubleQuotesInColumn = true;
                        }
                    } else {
                        curVal.append(ch);
                    }

                }
            } else {
                if (ch == customQuote) {
                    inQuotes = true;
                    if (chars[0] != '"' && customQuote == '\"') {
                        curVal.append('"');
                    }

                    if (startCollectChar) {
                        curVal.append('"');
                    }
                } else if (ch == separators) {

                    result.add(curVal.toString());

                    curVal = new StringBuffer();
                    startCollectChar = false;

                } else if (ch == '\r') {
                    continue;
                } else if (ch == '\n') {
                    break;
                } else {
                    curVal.append(ch);
                }
            }
        }

        result.add(curVal.toString());
        return result;
    }
}
